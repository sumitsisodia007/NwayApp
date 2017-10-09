using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace App2.Model
{
    public class CashFlowMdl
    {
        public string Tagtype { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
         [JsonProperty("list")]
        public ObservableCollection<CashFlowSiteListMdl> ListCashFlowSite { get; set; }
    }

    public class CashFlowSiteListMdl
    {
        [JsonProperty("company_id")]
        public string CompanyId { get; set; }
        public string Amt { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("amt_type")]
        public string AmtType { get; set; }
        [JsonProperty("site_data")]
        public ObservableCollection<SiteAmountMdl> ListSiteAccountMdls { get; set; }
    }

    public class SiteAmountMdl
    {
        [JsonProperty("site_id")]
        public int SiteId  { get; set; }
        [JsonProperty("site_name")]
        public string SiteName { get; set; }
        
        public string Amt { get; set; }
        [JsonProperty("amt_type")]
        public string AmtType { get; set; }
        [JsonProperty("account_data")]
        public ObservableCollection<SiteAccountTypeMdl> ListSiteAccountTypeMdls{ get; set; }
    }
    public class SiteAccountTypeMdl
    {
        [JsonProperty("account_head_id")]
        public int AccountHeadId { get; set; }
        [JsonProperty("account_head_name")]
        public string AccountHeadName { get; set; }
   
        public string Amt { get; set; }
        [JsonProperty("amt_type")]
        public string AmtType { get; set; }
        [JsonProperty("bank_data")]
        public ObservableCollection<SiteAccountBankMdl> ListSiteAccountBankMdl { get; set; }
    }
    public class SiteAccountBankMdl
    {
        [JsonProperty("account_id")]
        public int AccountId { get; set; }
        [JsonProperty("account_name")]
        public string AccountName { get; set; }
        [JsonProperty("amt")]
        public string Amt { get; set; }
        [JsonProperty("amt_type")]
        public string AmtType { get; set; }
    }
}
