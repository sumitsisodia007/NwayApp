using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
   public class NotificationListMdl
    {
        public string TagType { get; set; }
        public string Error { get; set; }
        //public List<NotificationDate> _notification_dates { get; set; }
        //public List<Tags> _tag { get; set; }
        //public List<Notifications> _notification{ get; set; }
    }

   public class NotificationDate
    {
        public string Date{ get; set; }
        public string NotCount{ get; set; }

    }
    public class Tags
    {
        public string Tag{ get; set; }
        public string NotCount{ get; set; }
        public string TotalAmt { get; set; }
    }
    public class Notifications
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
