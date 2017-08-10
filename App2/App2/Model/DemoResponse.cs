using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
    public class DemoResponse
    {
        public string Status { get; set; }
        public string Session_Key { get; set; }
        public string Session_Expire { get; set; }
        public string wt_Session_Ke { get; set; }
        public string wt_Email { get; set; }
        public string wt_Password { get; set; }
        public string wt_UserAct_Code { get; set; }
        public string wt_Challenge_Q { get; set; }
        public string wt_Challenge_A { get; set; }
    }
}