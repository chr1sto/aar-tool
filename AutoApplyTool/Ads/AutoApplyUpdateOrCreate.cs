using Google.Ads.GoogleAds;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V16.Resources;
using Google.Ads.GoogleAds.V16.Services;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoApplyTool.Ads
{
    public class AutoApplyUpdateOrCreate
    {
        //this will be run consecutively....
        public void Run(GoogleAdsClient client, 
            Dictionary<long, RecommendationSubscription[]> customerList, 
            RecommendationSubscription[] recommendationSubscriptions,
            long? loginCustomerId = null)
        {
            // If a manager ID is supplied, update the login credentials.
            if (loginCustomerId.HasValue)
            {
                client.Config.LoginCustomerId = loginCustomerId.ToString();
            }

            RecommendationSubscriptionServiceClient service = client.GetService(Services.V16.RecommendationSubscriptionService);

            FieldMask fieldMask = new FieldMask();
            fieldMask.Paths.AddRange(new string[] { "status" });

            foreach (var customer in customerList)
            {
                MutateRecommendationSubscriptionRequest request = new MutateRecommendationSubscriptionRequest()
                {
                    CustomerId = customer.Key.ToString(),
                    PartialFailure = true,
                    ValidateOnly = false,
                    ResponseContentType = Google.Ads.GoogleAds.V16.Enums.ResponseContentTypeEnum.Types.ResponseContentType.ResourceNameOnly
                };

                foreach (var subscription in recommendationSubscriptions)
                {
                    //Check if there is already a record for that specific recommendationSubscription
                    var existingRecords = customer.Value.Where(x => x.Type == subscription.Type);

                    RecommendationSubscriptionOperation operation;

                    //If Yes -> We have to do an update / Else just create
                    if (existingRecords.Count()  > 0)
                    {
                        var existingRecord = existingRecords.First();
                        operation = new RecommendationSubscriptionOperation()
                        {
                            UpdateMask = fieldMask,
                            Update = new RecommendationSubscription()
                            {
                                ResourceName = existingRecord.ResourceName,
                                Type = subscription.Type,
                                Status = subscription.Status
                            }
                        };
                    }
                    else
                    {
                        operation = new RecommendationSubscriptionOperation()
                        {
                            Create = subscription
                        };
                    }
                    request.Operations.Add(operation);
                }
                
                MutateRecommendationSubscriptionResponse response = service.MutateRecommendationSubscription(request);

                Console.WriteLine(response.ToString());
            }
        }
    }
}
