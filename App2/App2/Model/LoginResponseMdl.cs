using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
   public class LoginResponseMdl
    {
        public string Tagtype { get; set; }
        public string Error { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public int EmployeeId { get; set; }
        public string Message { get; set; }
        public float MinReceiptAmount { get; set; }
        public  int NotificationDayCount { get; set; }
        [JsonProperty("permissions")]
        public ObservableCollection<Permissions> _permissions { get; set; }
    }
    public class Permissions
    {
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("company_short_name")]
        public string CompanyShortName { get; set; }
        [JsonProperty("company_id")]
        public int CompanyId { get; set; }
        [JsonProperty("company_site")]
        public ObservableCollection<CompanySite> Sites { get; set; }
    }
    public class CompanySite
    {
        public string Site_name { get; set; }
        public string Site_short_name { get; set; }
        public int    Site_id		{ get; set; }
        public bool Chk_id { get; set; } = true;
    }
}
