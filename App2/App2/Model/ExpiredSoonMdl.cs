using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
    public class ExpiredSoonMdl
    {
        public string Tagtype { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        [JsonProperty("list")]
        public ObservableCollection<ExpiredSoonList> ExpiredSoonList { get; set; }
    }

    public class ExpiredSoonList
    {
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("termination_date")]
        public string TerminationDate { get; set; }
        [JsonProperty("booking_date")]
        public string BookngDate { get; set; }
        [JsonProperty("customer_booking_code")]
        public string BrandName { get; set; }
        [JsonProperty("unit_no")]
        public string UnitNo { get; set; }
    }
}