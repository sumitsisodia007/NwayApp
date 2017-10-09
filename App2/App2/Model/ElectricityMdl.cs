using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App2.Model
{
   public class ElectricityMdl
    {
        public double TxtWidth { get; set; }
        public string Particular { get; set; }
        public string OpeningReading { get; set; }
        public string ClosingReading { get; set; }
        public string Consumption { get; set; }

        [JsonProperty("list")]
        public ObservableCollection<ElectricitySiteMdl> ListElectricitySite { get; set; }
    }

    public class ElectricitySiteMdl
    {
        [JsonProperty("company_id")]
        public string CompanyId { get; set; }
        public string Amt { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("amt_type")]
        public string AmtType { get; set; }
    }
}
