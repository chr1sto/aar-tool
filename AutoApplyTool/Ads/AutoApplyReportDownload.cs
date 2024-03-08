using Google.Ads.GoogleAds;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V16.Errors;
using Google.Ads.GoogleAds.V16.Resources;
using Google.Ads.GoogleAds.V16.Services;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoApplyTool.Ads
{
    internal class AutoApplyReportDownload
    {
        private readonly string _queryType = "Auto Apply Subscriptions";
        private readonly string _GAQL_query = @"
            SELECT recommendation_subscription.create_date_time, 
                recommendation_subscription.modify_date_time, 
                recommendation_subscription.resource_name, 
                recommendation_subscription.status, 
                recommendation_subscription.type 
            FROM recommendation_subscription";

        /// <summary>
        /// Initiate all download requests, wait for their completion, and report the results.
        /// </summary>
        /// <param name="googleAdsService">The Google Ads service.</param>
        /// <param name="customerIds">The list of customer IDs from which to request data.</param>
        /// <returns>The asynchronous operation.</returns>
        public Dictionary<long, RecommendationSubscription[]> Run(GoogleAdsClient client, long[] customerIds, long? loginCustomerId = null)
        {
            // If a manager ID is supplied, update the login credentials.
            if (loginCustomerId.HasValue)
            {
                client.Config.LoginCustomerId = loginCustomerId.ToString();
            }

            // Get the GoogleAdsService. A single client can be shared by all threads.
            GoogleAdsServiceClient googleAdsService = client.GetService(
                Services.V16.GoogleAdsService);


            try
            {
                // Begin downloading reports and block program termination until complete.
                Task<ReportDownload[]> task = RunDownloadParallelAsync(googleAdsService, customerIds);
                task.Wait();

                Dictionary<long, RecommendationSubscription[]> customerRecommendationSubs = new Dictionary<long,RecommendationSubscription[]>();

                //Go through every entry in the requested Report Download and Append the RecommendationSubscriptions to an Array.
                foreach (var entry in task.Result)
                {
                    //Only Add the RecommendationSubscription if it was succesfull!
                    if(entry.Exception == null)
                    {
                        var recommendationSubs = entry.Response.Results.Select(x => x.RecommendationSubscription).ToArray();
                        customerRecommendationSubs.Add(entry.CustomerId, recommendationSubs);
                    }

                }

                return customerRecommendationSubs;
            }
            catch (GoogleAdsException e)
            {
                Console.WriteLine("Failure:");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"Failure: {e.Failure}");
                Console.WriteLine($"Request ID: {e.RequestId}");
                throw;
            }
        }

        private async Task<ReportDownload[]> RunDownloadParallelAsync(
           GoogleAdsServiceClient googleAdsService, long[] customerIds)
        {
            // List of all requests to ensure that we wait for the reports to complete on all
            // customer IDs before proceeding.
            ConcurrentBag<Task<bool>> tasks =
                new ConcurrentBag<Task<bool>>();

            // Collection of downloaded responses.
            ConcurrentBag<ReportDownload> responses = new ConcurrentBag<ReportDownload>();

            // IMPORTANT: You should avoid hitting the same customer ID in parallel. There are rate
            // limits at the customer ID level which are much stricter than limits at the developer
            // token level. Hitting these limits frequently enough will significantly reduce
            // throughput as the client library will automatically retry with exponential back-off
            // before failing the request.

            Parallel.ForEach(customerIds, customerId =>
            {
                //Replace for Logger
                Console.WriteLine($"Requesting {_queryType} for CID {_GAQL_query}.");

                // Issue an asynchronous search request and add it to the list of requests
                // in progress.
                tasks.Add(DownloadReportAsync(googleAdsService, customerId, _queryType,
                    _GAQL_query, responses));
            });

            Console.WriteLine($"Awaiting results from {tasks.Count} requests...\n");

            // Proceed only when all requests have completed.
            await Task.WhenAll(tasks);

            return responses.ToArray();
        }

        /// <summary>
        /// Initiates one asynchronous report download.
        /// </summary>
        /// <param name="googleAdsService">The Google Ads service client.</param>
        /// <param name="customerId">The customer ID from which data is requested.</param>
        /// <param name="queryKey">The name of the query to be downloaded.</param>
        /// <param name="queryValue">The query for the download request.</param>
        /// <param name="responses">Collection of all successful report downloads.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="GoogleAdsException">Thrown if errors encountered in the execution of
        ///     the request.</exception>
        private Task<bool> DownloadReportAsync(
            GoogleAdsServiceClient googleAdsService, long customerId, string queryKey,
            string queryValue, ConcurrentBag<ReportDownload> responses)
        {
            try
            {
                // Issue an asynchronous download request.
                googleAdsService.SearchStream(
                    customerId.ToString(), queryValue,
                    delegate (SearchGoogleAdsStreamResponse resp)
                    {
                        // Store the results.
                        responses.Add(new ReportDownload()
                        {
                            CustomerId = customerId,
                            QueryKey = queryKey,
                            Response = resp
                        });
                    }
                );
                return Task.FromResult(true);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Download failed for {queryKey} and CID {customerId}!");

                GoogleAdsException gae = GoogleAdsException.FromTaskException(ae);

                var download = new ReportDownload()
                {
                    CustomerId = customerId,
                    QueryKey = queryKey,
                    Exception = gae
                };
                if (gae != null)
                {
                    Console.WriteLine($"Message: {gae.Message}");
                    Console.WriteLine($"Failure: {gae.Failure}");
                    Console.WriteLine($"Request ID: {gae.RequestId}");
                    download.Exception = gae;
                }
                else
                {
                    download.Exception = ae;
                }

                responses.Add(download);
                return Task.FromResult(false);
            }
        }

        private class ReportDownload
        {
            internal long CustomerId { get; set; }
            internal string QueryKey { get; set; }
            internal SearchGoogleAdsStreamResponse Response { get; set; }
            internal Exception Exception { get; set; }

            public override string ToString()
            {
                if (Exception != null)
                {
                    return $"Download failed for {QueryKey} and CID {CustomerId}. " +
                        $"Exception: {Exception}";
                }
                else
                {
                    return $"{QueryKey} downloaded for CID {CustomerId}: " +
                        $"{Response.Results.Count} rows returned.";
                }
            }
        }

    }
}
