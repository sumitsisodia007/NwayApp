using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{

        public class NotificationListMdl
        {
            public string TagType { get; set; }
            public string Error { get; set; }
            [JsonProperty("list")]
            public ObservableCollection<NotificationDate> ListNotificationDate { get; set; }
            public string Message { get; set; }
        }
    
        public class NotificationDate
        {
            public string Date { get; set; }
            public string NotCount { get; set; }
            [JsonProperty("tags")]
            public ObservableCollection<Tags> ListTags { get; set; }
        }

        public class Tags
        {
            public string Tag { get; set; }
            public string NotCount { get; set; }
            public string Total_Amount { get; set; }
            [JsonProperty("notifications")]
            public ObservableCollection<Notification> Notification { get; set; }
           
        }

        public class Notification
        {
            //Cancellation
            public string Tagtype { get; set; }
            public string Information_type { get; set; }
            public string Invoice_code { get; set; }
            public string Invoice_date { get; set; }
            public string Customer_name { get; set; }
            public string Customer_id { get; set; }
            public string Cancelled_by { get; set; }
            public string Cancelled_by_id { get; set; }
            //Receipt and Paid
            public string Company_name { get; set; }
            public string Site_name { get; set; }
            public string Site_id { get; set; }
            public string Party_id { get; set; }
            public string Party_name { get; set; }
            public string Party_outstanding { get; set; }
            public string Amount_received { get; set; }
            public string Current_outstanding { get; set; }
            //Invoice event
            public string Invoce_Code { get; set; }
            public string Invoce_Date { get; set; }
            public string Converted_To { get; set; }
            public string Event_Date { get; set; }
            //Filter Date
            public string Filter_Date { get; set; }
    }   

     
}
