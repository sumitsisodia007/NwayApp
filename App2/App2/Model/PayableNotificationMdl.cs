using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
    public class PayableNotificationMdl
    {
        public string TagType { get; set; }
        public string Error { get; set; }
        [JsonProperty("list")]
        public ObservableCollection<Payablelistmdl> ListPayablemdl { get; set; }
        [JsonProperty("notification")]
        public ObservableCollection<PayableNotificationDate> ListPayableNotification { get; set; }
        public string Message { get; set; }
    }
    public class Payablelistmdl
    {
       
        public string Site_name { get; set; }
       
        public string Total_dr { get; set; }
       
        public string Total_cr { get; set; }
       
        public string Balance { get; set; }
    }

    public class PayableNotificationDate
    {
        public string Date { get; set; }
        public string NotCount { get; set; }
        [JsonProperty("tags")]
        public ObservableCollection<PayableNotificationTags> ListPayablemdl { get; set; }
    }

    public class PayableNotificationTags
    {
        public string Tag { get; set; }
        public string NotCount { get; set; }
        public string Total_Amount { get; set; }
        [JsonProperty("notifications")]
        public ObservableCollection<PayableNotification> Notification { get; set; }
    }

    public class PayableNotification
    {
        public string Company_name { get; set; }
        public string Site_name { get; set; }
        public string Site_id { get; set; }
        public string Party_id { get; set; }
        public string Party_name { get; set; }
        public string Party_outstanding { get; set; }
        public string Amount_received { get; set; }
        public string Current_outstanding { get; set; }
        public string Tagtype { get; set; }
        public string Information_type { get; set; }
    }
}
