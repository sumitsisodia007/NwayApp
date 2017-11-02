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
        public string Error { get; set; }
        public string Message { get; set; }
        [JsonProperty("list")]
        public ObservableCollection<ExpiredSoonList> ExpiredSoonList { get; set; }
    }

    public class ExpiredSoonList
    {
        public string CustomerName { get; set; }
        public string TerminationDate { get; set; }
        public string BookngDate { get; set; }
        public string BrandName { get; set; }
        public string UnitNo { get; set; }
    }
}