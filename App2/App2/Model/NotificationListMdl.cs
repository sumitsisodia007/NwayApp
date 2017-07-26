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
        public class TestCases
        {
            public string TagType { get; set; }
            public string Error { get; set; }
            public string Message { get; set; }
            public ObservableCollection<NotificationDate> ListNotificationDate { get; set; }
        }
    
        public class NotificationDate
        {
            public string Date { get; set; }
            public string NotCount { get; set; }
            public ObservableCollection<Tags> ListTags { get; set; }
        }

        public class Tags
        {
            public string Tag { get; set; }
            public string TagNotCount { get; set; }
            public string TotalAmt { get; set; }
            public string Date { get; set; }
            public ObservableCollection<Receipt_Notifications> List_Receipt { get; set; }
            public ObservableCollection<Cancelletion_Notifications> List_Cancellation { get; set; }
            public ObservableCollection<Paid_Notifications> List_Paid { get; set; }
            public ObservableCollection<Invoice_Event> List_Invoice { get; set; }
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
            public string Date { get; set; }
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
            public string Date { get; set; }
        }

        public class Cancelletion_Notifications
        {
            public string CancelInvoice_code { get; set; }
            public string CancelInvoice_date { get; set; }
            public string CancelCustomer_name { get; set; }
            public string CancelCustomer_id { get; set; }
            public string CancelCancelled_by { get; set; }
            public string CancelCancelled_by_id { get; set; }
            public string CancelInformation_type { get; set; }
            public string CancelTagtype { get; set; }
            public string Date { get; set; }
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
             public string Date { get; set; }
        }
    }
}
