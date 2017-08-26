using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Helper
{
    public class EnumMaster
    {
        public static readonly string SIGNIN = "signin";
        public static readonly string SETTINGS = "settings";
        public static readonly string TAGTYPENOTIFICATIONS = "notifications";
        public static readonly string TAGTYPEPAYABLE_OUTSTANDING = "payable_outstanding";
        public static readonly string TAGTYPERECEIVABLE_OUTSTANDING = "receivable_outstanding";
        public static readonly string TAGTYPEINVOICE_CANCELLETION = "invoice_cancelletion";
        public static readonly string C21_MALHAR= "CENTURY 21 TOWN PLANNERS PVT. LTD.";
        
        public static readonly string LblChartTitle= "(Monthly Ledger)";

        public static readonly string LblReceivedFirstTitle= "Today Collection(Notification)";
        public static readonly string LblReceivedSecoundTitle = "Total OutStanding";
        public static readonly string LblReceivedOutstanding= "Pre Outstanding";
        public static readonly string LblReceivedTodayPaid= "Today Receipt";
        public static readonly string LblReceivedCurOutstanding= "Cur. Outstanding";
        public static readonly string LblReceivedSiteName = "Particular";
        public static readonly string LblReceivedTotaleDr = "Total Due";
        public static readonly string LblReceivedTotaleCr = "Receive";
        public static readonly string LblPaidFirstTitle = "Today Payment(SETTING amount) (NOTIFICATION)";
        public static readonly string LblPaidSecoundTitle = "Total Payable";
        public static readonly string LblPaidReceivedParty = "Party";
        public static readonly string LblPaidReceivedBalance = "Balance";
        public static readonly string LblPaidOutstanding = "Outstanding";
        public static readonly string LblPaidTodayPaid = "Today Paid";
        public static readonly string LblPaidCurOutstanding = "Cur. Outstanding";
        public static readonly string LblPaidSiteName = "Site Name";
        public static readonly string LblPaidTotaleDr = "Total Dr.";
        public static readonly string LblPaidTotaleCr = "Total Cr.";
    }
}
