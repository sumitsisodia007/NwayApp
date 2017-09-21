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
        public int User_id { get; set; }
        public string User_name { get; set; }
        public string User_type { get; set; }
        public int Employee_id { get; set; }
        public string Message { get; set; }
        public float Min_receipt_amount { get; set; }
        public  int Notification_day_count { get; set; }
        [JsonProperty("permissions")]
        public ObservableCollection<Permissions> _permissions { get; set; }
    }
    public class Permissions
    {
        public string Company_name { get; set; }
        public string Company_short_name  { get; set; }
        public int    Company_id			{ get; set; }
        [JsonProperty("company_site")]
        public ObservableCollection<Company_site> _company_site { get; set; }
    }
    public class Company_site
    {
        public string Site_name { get; set; }
        public string Site_short_name { get; set; }
        public int    Site_id		{ get; set; }
        public bool Chk_id { get; set; } = true;
    }
}
