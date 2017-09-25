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
        public string MinReceiptAmt { get; set; }
        public string NotificationDayCount { get; set; }
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string NotCount { get; set; }
        public string NotCountDate { get; set; }
        public string CompanyIndex { get; set; }
        public string CompanyName { get; set; }
    }
}
