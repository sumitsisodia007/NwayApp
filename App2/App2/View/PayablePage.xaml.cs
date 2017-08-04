using App2.APIService;
using App2.Helper;
using App2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PayablePage : ContentPage
    {
        public List<ShowPayableTodayDetail> _payableshowlist { get; set; }
        public List<ShowPayableTotalPayble> _showpayabletotalpayblelist { get; set; }
        public List<PayableNotificationMdl> _payablenotificationdata { get; set; }
        NavigationMdl navmdl;
        PayableNotificationMdl _payable;
        public double _Width = 0;
        public static int flag = 0;
        API api = new API();

        public PayablePage()
        {
            InitializeComponent();
        }

       public PayablePage (string title)
	   {
			InitializeComponent ();
            this.Title = title;
            navmdl = new NavigationMdl();
            navmdl.User_id = "";
            navmdl.Device_id = "32132";
            navmdl.Company_name = EnumMaster.C21_MALHAR;
            
            if (title == "Receivable")
            {
                PredefinedReceived();
                navmdl.Tag_type = EnumMaster.RECEIVABLE_OUTSTANDING;
            }
            else
            {
                PredefinedPaid();
                navmdl.Tag_type = EnumMaster.PAYABLE_OUTSTANDING;
            }
            //calling api
            _payable = api.PayableTable(navmdl);

            ShowPayableTodayDetail toady_notification = new ShowPayableTodayDetail();            
            flag = 1;
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                lblparty.WidthRequest =lbloutstanding.WidthRequest =lblTodayReceipt.WidthRequest =lblCurOutstanding.WidthRequest = _Width = calcScreenWidth / 4 - 10;
            }
            ShowPaybleToday();
            ShowTotalPayble();
        }
        private void PredefinedPaid()
        {
            lblFirstTitle.Text = EnumMaster.LblPaidFirstTitle;
            lblSecoundTitle.Text = EnumMaster.LblPaidSecoundTitle;
            lblparty.Text = EnumMaster.LblPaidReceivedParty;
            lbloutstanding.Text = EnumMaster.LblPaidOutstanding;
            lblTodayReceipt.Text = EnumMaster.LblPaidTodayPaid;
            lblCurOutstanding.Text = EnumMaster.LblPaidCurOutstanding;
            lblSiteName.Text = EnumMaster.LblPaidSiteName;
            lblTotalDr.Text = EnumMaster.LblPaidTotaleDr;
            lblTotalCr.Text = EnumMaster.LblPaidTotaleCr;
            lblBalance.Text = EnumMaster.LblPaidReceivedBalance;
            this.BackgroundColor = Color.FromHex("#ED7D31");
            lblparty.BackgroundColor = lbloutstanding.BackgroundColor = lblTodayReceipt.BackgroundColor = lblCurOutstanding.BackgroundColor = lblSiteName.BackgroundColor = lblTotalDr.BackgroundColor = lblTotalCr.BackgroundColor = lblBalance.BackgroundColor = Color.FromHex("#ED7D31");
        }
        private void PredefinedReceived()
        {
            lblFirstTitle.Text = EnumMaster.LblReceivedFirstTitle;
            lblSecoundTitle.Text = EnumMaster.LblReceivedSecoundTitle;
            lblparty.Text = EnumMaster.LblPaidReceivedParty;
            lbloutstanding.Text = EnumMaster.LblReceivedOutstanding;
            lblTodayReceipt.Text = EnumMaster.LblReceivedTodayPaid;
            lblCurOutstanding.Text = EnumMaster.LblReceivedCurOutstanding;
            lblSiteName.Text = EnumMaster.LblReceivedSiteName;
            lblTotalDr.Text = EnumMaster.LblReceivedTotaleDr;
            lblTotalCr.Text = EnumMaster.LblReceivedTotaleCr;
            lblBalance.Text = EnumMaster.LblPaidReceivedBalance;
            this.BackgroundColor = Color.FromHex("#A3C1E5");
            lblparty.BackgroundColor = lbloutstanding.BackgroundColor = lblTodayReceipt.BackgroundColor = lblCurOutstanding.BackgroundColor = lblSiteName.BackgroundColor = lblTotalDr.BackgroundColor = lblTotalCr.BackgroundColor = lblBalance.BackgroundColor = Color.FromHex("#A3C1E5");
        }

        public void ShowPaybleToday()
        {
            _payableshowlist = new List<ShowPayableTodayDetail>();
            foreach (var item in _payable.ListPayableNotification)
            {
                foreach (var item2 in item.ListPayablemdl)
                {
                    foreach (var item3 in item2.Notification)
                    {
                        _payableshowlist.Add(new ShowPayableTodayDetail() {Show_Party_Id=item3.Party_id, txtWidth = _Width, Show_Pay_Party = item3.Party_name, Show_Pay_Outstanding = item3.Party_outstanding, Show_Amount_Received = item3.Amount_received, Show_Cur_Outstanding = item3.Current_outstanding });
                    }
                }
            }
            list_today_payble.ItemsSource = _payableshowlist;
        }

        public void ShowTotalPayble()
        {
            _showpayabletotalpayblelist = new List<ShowPayableTotalPayble>();
            foreach (var item in _payable.ListPayablemdl)
            {
                if (lblSiteName.Text != "Particular")
                {
                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = item.Site_name, Show_Balance = item.Balance, Show_Total_cr = item.Total_cr, Show_Total_dr = item.Total_dr });
                }
                else
                {
                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = item.Perticular, Show_Balance = item.Balance, Show_Total_cr = item.Total_Due, Show_Total_dr = item.Receive });
                }
            }
            list_totalpayble.ItemsSource = _showpayabletotalpayblelist;
        }

        private void list_today_payble_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            navmdl = new NavigationMdl();
            ShowPayableTodayDetail toady_notification = (ShowPayableTodayDetail)e.Item;
           
            navmdl.Party_id = toady_notification.Show_Party_Id;
            navmdl.Device_id = "32132";
            navmdl.Company_name = EnumMaster.C21_MALHAR;
            navmdl.Party_Name = toady_notification.Show_Pay_Party;
            if (this.Title== "Receivable")
            { navmdl.Tag_type = EnumMaster.RECEIVABLE_OUTSTANDING; }
            else { navmdl.Tag_type = EnumMaster.PAYABLE_OUTSTANDING; }
            Navigation.PushAsync(new PayableChart(navmdl));
        }
    }
    public class ShowPayableTodayDetail
    {
        public string Show_Pay_Party { get; set; }
        public string Show_Pay_Outstanding { get; set; }
        public string Show_Amount_Received { get; set; }
        public string Show_Cur_Outstanding { get; set; }
        public string Show_Party_Id { get; set; }
        public double txtWidth { get; set; }
    }

    public class ShowPayableTotalPayble
    {
        public string Show_Site_name { get; set; }
        public string Show_Total_dr { get; set; }
        public string Show_Total_cr { get; set; }
        public string Show_Balance { get; set; }
        public double txtWidth { get; set; }
    }

}