using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.ShowModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        PartysearchMdl lstLoca = null;
        bool isListSelected = false;
        ShowPayableTodayDetail toady_notification;
        NavigationMdl navmdl;
        PayableNotificationMdl _payable;
        public double _Width = 0;
        public static int flag = 0;
        API api = new API();

        public PayablePage()
        {
            InitializeComponent();
        }

       public PayablePage (NavigationMdl mdl)
	   {
			InitializeComponent ();
           // NavigationPage.SetHasNavigationBar(this, false);
            //NavigationPage.SetTitleIcon(this, "icon.png");
            this.Title = mdl.Page_Title;
            navmdl = new NavigationMdl();
          
            
            if (mdl.Page_Title == "Receivable")
            {
                PredefinedReceived();
               // navmdl.Tag_type = EnumMaster.RECEIVABLE_OUTSTANDING;
            }
            else
            {
                PredefinedPaid();
               // navmdl.Tag_type = EnumMaster.PAYABLE_OUTSTANDING;
            }
            //calling api
            _payable = api.PayableTable(mdl);

             toady_notification = new ShowPayableTodayDetail();            
            flag = 1;
            try
            {
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
                {
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;
                    lblparty.WidthRequest = lbloutstanding.WidthRequest = lblTodayReceipt.WidthRequest = lblCurOutstanding.WidthRequest = _Width = calcScreenWidth / 4 - 10;
                }
            }
            catch (Exception ex)
            {
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
                    if (item2.NotCount == "0")
                    {
                        stktodaypayable.IsVisible = false;
                    }
                    else
                    {
                        stktodaypayable.IsVisible = true;
                    }
                    foreach (var item3 in item2.Notification)
                    {
                        string tmp;
                        if (Convert.ToInt32(item3.Party_name.Length) >= 9)
                        {
                            tmp = item3.Party_name.Substring(0, 10);
                        }
                        else
                        {
                            tmp = item3.Party_name;
                        }

                        _payableshowlist.Add(new ShowPayableTodayDetail() {Show_Party_Id=item3.Party_id, txtWidth = _Width, Show_Pay_Party =tmp+"..", Show_Pay_Outstanding = item3.Party_outstanding, Show_Amount_Received = item3.Amount_received, Show_Cur_Outstanding = item3.Current_outstanding });
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
                string tmp = null;
                if (lblSiteName.Text != "Particular")
                {
                    if (Convert.ToInt32(item.Site_name.Length) >= 9)
                    {
                        tmp = item.Site_name.Substring(0, 10);
                    }
                    else
                    {
                        tmp = item.Site_name;
                    }
                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = tmp+"..", Show_Balance = item.Balance, Show_Total_cr = item.Total_cr, Show_Total_dr = item.Total_dr });
                }
                else
                {

                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = item.Perticular, Show_Balance = item.Balance, Show_Total_cr = item.Receive, Show_Total_dr = item.Total_Due});
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

        private async void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                isListSelected = false;
                if (e.NewTextValue != string.Empty)
                {
                    navmdl = new NavigationMdl();
                    navmdl.User_id = "1";
                    navmdl.Device_id = "123456";//StaticMethods.getDeviceidentifier();
                    navmdl.Company_name = Helper.EnumMaster.C21_MALHAR;
                    navmdl.Party_Name = e.NewTextValue;
                    navmdl.Tag_type = "partylist";
                    lstLoca = new PartysearchMdl();
                    ObservableCollection<PartysearchlistMdl> _lst = null;
                    _lst = new ObservableCollection<PartysearchlistMdl>();
                    api = new API();
                    lstLoca = await api.GetParty(navmdl);

                    foreach (var item in lstLoca.Party_List)
                    {
                        _lst.Add(new PartysearchlistMdl { Party_Id = item.Party_Id, Party_Name = item.Party_Name });
                    }
                    AutoList.ItemsSource = _lst;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (_lst.Count > 0)
                        {
                            AutoList.IsVisible = true;
                            if(AutoList.IsVisible == true)
                            {
                                imgLogo.IsVisible = false;
                            }
                           // AutoList.ItemsSource = _lst.Select(c => { c.txtWidth = ScreenWidth; return c; }).ToList();
                            AutoList.HeightRequest = 40 * 5;
                        }
                        else
                        {
                            AutoList.ItemsSource = null;
                            AutoList.IsVisible = false;
                        }
                           //await scrollbar.ScrollToAsync(txtAuto, ScrollToPosition.Start, true);
                    });
                   
                }
                else
                {
                    AutoList.IsVisible = false;
                    imgLogo.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.ShowToast(ex.Message);
            }
        }

        private async void txtAuto_Focused(object sender, FocusEventArgs e)
        {
            await Task.Delay(100);
            
        }
        async void txtLocation_Focus(object sender, EventArgs args)
        {
            await Task.Delay(2000);
            txtAuto.Unfocus();
            txtAuto.Focus();
        }

        private void txtAuto_Unfocused(object sender, FocusEventArgs e)
        {
            if (!isListSelected)
            {
                txtAuto.TextChanged -= txtAuto_TextChanged;
                txtAuto.Text = string.Empty;
                txtAuto.TextChanged += txtAuto_TextChanged;
            }
            
            AutoList.IsVisible = false;
            imgLogo.IsVisible = true;
            //PM 18-2-2017
            txtAuto.Placeholder = "Select Party";

        }

        private void AutoList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            PartysearchlistMdl obj = null;
            navmdl = new NavigationMdl();
            try
            {
                isListSelected = true;
                AutoList.IsVisible = false;
                txtAuto.TextChanged -= txtAuto_TextChanged;
                txtAuto.Unfocus();
                obj = (PartysearchlistMdl)e.Item;
               
                navmdl.Party_id = obj.Party_Id;
                navmdl.Device_id = "32132";
                navmdl.Company_name = EnumMaster.C21_MALHAR;
                navmdl.Party_Name = obj.Party_Name;
                if (this.Title == "Receivable")
                { navmdl.Tag_type = EnumMaster.RECEIVABLE_OUTSTANDING; }
                else { navmdl.Tag_type = EnumMaster.PAYABLE_OUTSTANDING; }
                Navigation.PushAsync(new PayableChart(navmdl));
            }
            catch (Exception ex)
            {
                //StaticMethods.ShowToast(ex.Message);
            }
            finally
            {
                obj = null;
                txtAuto.TextChanged += txtAuto_TextChanged;
            }
        }
    }
   
}