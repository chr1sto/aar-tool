using Google.Ads.GoogleAds.Config;
using Google.Ads.GoogleAds.Lib;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using AutoApplyTool.Ads;
using Google.Ads.GoogleAds.V16.Resources;
using Google.Ads.GoogleAds.V16.Enums;

namespace AutoApplyTool
{
    public partial class AutoApplyForm : Form
    {
        private readonly GoogleAdsClient _client;

        private Dictionary<long, RecommendationSubscription[]> customerRecommendationSubscriptions = new Dictionary<long, RecommendationSubscription[]>();

        private readonly Dictionary<string, Google.Ads.GoogleAds.V16.Enums.RecommendationTypeEnum.Types.RecommendationType> _recTypeMapping = new Dictionary<string, Google.Ads.GoogleAds.V16.Enums.RecommendationTypeEnum.Types.RecommendationType>()
        {
            { "DISPLAY_EXPANSION_OPT_IN", RecommendationTypeEnum.Types.RecommendationType.DisplayExpansionOptIn },
            { "KEYWORD", RecommendationTypeEnum.Types.RecommendationType.Keyword },
            { "USE_BROAD_MATCH_KEYWORD", RecommendationTypeEnum.Types.RecommendationType.UseBroadMatchKeyword },
            { "MAXIMIZE_CLICKS_OPT_IN", RecommendationTypeEnum.Types.RecommendationType.MaximizeClicksOptIn },
            { "MAXIMIZE_CONVERSIONS_OPT_IN", RecommendationTypeEnum.Types.RecommendationType.MaximizeConversionsOptIn},
            { "MAXIMIZE_CONVERSION_VALUE_OPT_IN", RecommendationTypeEnum.Types.RecommendationType.MaximizeConversionValueOptIn },
            { "OPTIMIZE_AD_ROTATION", RecommendationTypeEnum.Types.RecommendationType.OptimizeAdRotation },
            { "RAISE_TARGET_CPA", RecommendationTypeEnum.Types.RecommendationType.RaiseTargetCpa },
            { "LOWER_TARGET_ROAS", RecommendationTypeEnum.Types.RecommendationType.LowerTargetRoas},
            { "SET_TARGET_CPA", RecommendationTypeEnum.Types.RecommendationType.SetTargetCpa },
            { "SET_TARGET_ROAS", RecommendationTypeEnum.Types.RecommendationType.SetTargetRoas },
            { "SEARCH_PARTNERS_OPT_IN", RecommendationTypeEnum.Types.RecommendationType.SearchPartnersOptIn},
            { "TARGET_CPA_OPT_IN", RecommendationTypeEnum.Types.RecommendationType.TargetCpaOptIn },
            { "TARGET_ROAS_OPT_IN", RecommendationTypeEnum.Types.RecommendationType.TargetRoasOptIn },
            { "RESPONSIVE_SEARCH_AD", RecommendationTypeEnum.Types.RecommendationType.ResponsiveSearchAd },
            { "RESPONSIVE_SEARCH_AD_IMPROVE_AD_STRENGTH", RecommendationTypeEnum.Types.RecommendationType.ResponsiveSearchAdImproveAdStrength }
        };

        private bool customerListLoaded = false;

        public AutoApplyForm()
        {
            GoogleAdsConfig config = new GoogleAdsConfig()
            {
                DeveloperToken = "",
                OAuth2Mode = Google.Ads.Gax.Config.OAuth2Flow.APPLICATION,
                OAuth2ClientId = "",
                OAuth2ClientSecret = "",
                OAuth2RefreshToken = ""
            };

            _client = new GoogleAdsClient(config);

            InitializeComponent();
        }

        void Test()
        {
            AutoApplyReportDownload download = new AutoApplyReportDownload();
            var result = download.Run(_client, new[] { 9824336198, 4781613407 }, 5639584166);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cids = this.cidInputTextBox.Text.Split(Environment.NewLine).Select(x =>
            {
                var escapedCid = x.Trim().Replace("-", "");
                if (long.TryParse(escapedCid, out long result)) return result;
                return 0;
            }).Where(y => y != 0)
            .ToArray();

            var escapedLoginId = this.loginCustomerIdTextBox.Text.Trim().Replace("-", "");
            long.TryParse(escapedLoginId, out long loginCustomerId);

            AutoApplyReportDownload autoApplyDownload = new AutoApplyReportDownload();
            customerRecommendationSubscriptions = autoApplyDownload.Run(_client, cids, loginCustomerId);

            this.cidInputTextBox.Visible = false;
            this.dataGridView1.Visible = true;

            var customerRecSubsViewModel = from c in customerRecommendationSubscriptions select new { CustomerId = c.Key, Activated = c.Value.Where(z => z.Status == Google.Ads.GoogleAds.V16.Enums.RecommendationSubscriptionStatusEnum.Types.RecommendationSubscriptionStatus.Enabled).Count() };
            this.dataGridView1.DataSource = customerRecSubsViewModel.ToArray();

            this.checkedListBox1.Enabled = true;
            this.button1.Enabled = true;

            customerListLoaded = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<RecommendationSubscription> subscriptions = new List<RecommendationSubscription>();
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                string text = this.checkedListBox1.Items[i].ToString();
                bool enabled = this.checkedListBox1.GetItemChecked(i);
                RecommendationSubscription subItem = getRecommendationSubscription(text, enabled);
                subscriptions.Add(subItem);
            }

            AutoApplyUpdateOrCreate updateOrCreate = new AutoApplyUpdateOrCreate();

            var escapedLoginId = this.loginCustomerIdTextBox.Text.Trim().Replace("-", "");
            long.TryParse(escapedLoginId, out long loginCustomerId);
            updateOrCreate.Run(_client,customerRecommendationSubscriptions,subscriptions.ToArray(), loginCustomerId);
        }

        private RecommendationSubscription getRecommendationSubscription(string text, bool enabled)
        {

            RecommendationSubscription result;
            
            if (_recTypeMapping.TryGetValue(text, out RecommendationTypeEnum.Types.RecommendationType recType))
            {
                result = new RecommendationSubscription()
                {
                    Type = recType,
                    Status = enabled
                            ? RecommendationSubscriptionStatusEnum.Types.RecommendationSubscriptionStatus.Enabled
                            : RecommendationSubscriptionStatusEnum.Types.RecommendationSubscriptionStatus.Paused
                };
            }
            else
            {
                result = new RecommendationSubscription()
                {
                    Type = RecommendationTypeEnum.Types.RecommendationType.Unknown,
                    Status = RecommendationSubscriptionStatusEnum.Types.RecommendationSubscriptionStatus.Unknown
                };
            }
            return result;
        }
    }
}
