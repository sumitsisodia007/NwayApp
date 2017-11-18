using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
   public class CancellationMdl
    {
        public string Tagtype { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        [JsonProperty("list")]
        public ObservableCollection<CancellationList> CancellationList { get; set; }
    }

    public class CancellationList
    {
        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("cancel_date")]
        public string CancellationDate { get; set; }
        [JsonProperty("invoice_date")]
        public string InvoiceDate { get; set; }
        [JsonProperty("invoice_code")]
        public string InvoiceCode { get; set; }
         [JsonProperty("invoice_type")]
        public string InvoiceType { get; set; }
    }
}
