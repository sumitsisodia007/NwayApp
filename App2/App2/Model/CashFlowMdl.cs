using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace App2.Model
{
    public class CashFlowMdl
    {
        public string CompanyName{ get; set; }
        public double CompanyAmount { get; set; }
       // [JsonProperty("permissions")]
        public ObservableCollection<CashFlowSiteMdl> ListFlowSiteMdls{ get; set; }
    }

    public class CashFlowSiteMdl
    {
        public string SiteName { get; set; }
        public double SiteAmount { get; set; }
        public string SiteBank { get; set; }
    }
}
