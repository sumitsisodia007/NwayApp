using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
   public class ResponseModel
    {
        public string TagType { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Min_Receipt_Amt { get; set; }
        public string Notification_Day_Count { get; set; }
        public string User_Id { get; set; }
        public string Device_Id { get; set; }
        public string DeviceToken { get; set; }
        public string NotCount { get; set; }
        public string NotCountDate { get; set; }
    }
}
