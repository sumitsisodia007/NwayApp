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
        [JsonProperty("tagtype")]
        public string Tagtype { get; set; }
        [JsonProperty("error")]
        public bool Error { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        [JsonProperty("user_type")]
        public string UserType { get; set; }
        [JsonProperty("employee_id")]
        public int EmployeeId { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("min_receipt_amount")]
        public float MinReceiptAmount { get; set; }
        [JsonProperty("notification_day_count")]
        public  int NotificationDayCount { get; set; }

        [JsonProperty("expire_day_count")]
        public string ExpireDayCount { get; set; }
        [JsonProperty("invoice_cancel_day_count")]
        public string InvoiceCancelDayCount { get; set; }


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
        public string ImgName { get; set; } = "on_btn";
    }
}
