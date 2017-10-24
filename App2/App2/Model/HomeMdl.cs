using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App2.Model
{
    public class HomeMdl
    {
        public string tagtype { get; set; }
        public bool error { get; set; }
        [JsonProperty("list")]
        public List<MainHomeDetails> ListHomeDetails { get; set; }
        public string message { get; set; }
    }
    public class MainHomeDetails
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string bank { get; set; }
        public string electric_consumption { get; set; }
        public string payable { get; set; }
        public string receivable { get; set; }
        public int expire { get; set; }
        public int cancellation { get; set; }
        [JsonProperty("notification_count")]
        public int notificationCount { get; set; }
    }
}
