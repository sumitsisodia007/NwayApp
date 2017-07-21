using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
    public class NotificationListMdl
    {
        public class TestCases
        {
            public List<RootObject> rootObjects { get; set; }
        }
        public class RootObject
        {
            public string TagType { get; set; }
            public string Error { get; set; }
            public string Message { get; set; }
            public List<NotificationDate> _notification_dates { get; set; }
        }

        public class NotificationDate
        {
            public string Date { get; set; }
            public string NotCount { get; set; }
            public List<Tags> _tag { get; set; }
        }
        public class Tags
        {
            public string Tag { get; set; }
            public string NotCount { get; set; }
            public string TotalAmt { get; set; }
            public List<Receipt_Notifications> _list_receipt { get; set; }
            public List<Cancelletion_Notifications> _list_Cancellation { get; set; }
            public List<Paid_Notifications> _list_Paid { get; set; }
            public List<Invoice_Event> _list_ivoce { get; set; }
        }

        public class Receipt_Notifications
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
        public class Paid_Notifications
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
        public class Cancelletion_Notifications
        {
            public string Invoice_code { get; set; }
            public string Invoice_date { get; set; }
            public string Customer_name { get; set; }
            public string Customer_id { get; set; }
            public string Cancelled_by { get; set; }
            public string Cancelled_by_id { get; set; }
            public string Information_type { get; set; }
            public string Tagtype { get; set; }
        }
        public class Invoice_Event
        {
            public string Company_name { get; set; }
            public string Site_name { get; set; }
            public string Site_id { get; set; }
            public string Information_type { get; set; }
            public string Party_id { get; set; }
            public string Party_name { get; set; }
            public string Tagtype { get; set; }
            public string Invoce_Code { get; set; }
            public string Invoce_Date { get; set; }
            public string Converted_To { get; set; }
            public string Event_Date { get; set; }

        }
    }
}
