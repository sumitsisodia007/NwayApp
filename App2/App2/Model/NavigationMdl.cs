﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
   public class NavigationMdl
    {
        public string User_id { get; set; }
        public string Company_name { get; set; }
        public string Device_id { get; set; }
        public string Tag_type { get; set; }
        public string Party_id { get; set; }
        public string Party_Name { get; set; }
        public string Min_Receipt_Amount { get; set; }
        public string Notification_Day_Count { get; set; }
        public string Page_Title { get; set; }
        public bool Is_Notification{ get; set; }
    }
}
