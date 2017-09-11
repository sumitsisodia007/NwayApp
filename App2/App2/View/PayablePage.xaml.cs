using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.ShowModels;
using Microcharts;
using SkiaSharp;
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
        
        public bool Notflag { get; set; }
        PartysearchMdl lstLoca = null;
        bool isListSelected = false;
        ShowPayableTodayDetail toady_notification;
        NavigationMdl navmdl;
       
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
            MAinMethods(mdl);
            flag = 1;
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    Notflag = mdl.Is_Notification;

              
            //    navmdl = new NavigationMdl();
            //    if (mdl.Page_Title == "Receivable")
            //    {
            //        PredefinedReceived();
            //    }
            //    else
            //    {
            //        PredefinedPaid();
            //    }
            //    ResponseModel rs = StaticMethods.GetLocalSavedData();
            //    mdl.User_id = rs.User_Id;
            //    PayableNotificationMdl _payable = await api.PayableTable(mdl);

            //    toady_notification = new ShowPayableTodayDetail();
            //    flag = 1;

            //    ShowPaybleToday(_payable);
            //    ShowTotalPayble(_payable);
            //});
        }




        private async void MAinMethods(NavigationMdl mdl)
        {
            navmdl = new NavigationMdl();
            if (mdl.Page_Title == "Receivable")
            {
                PredefinedReceived();
            }
            else
            {
                PredefinedPaid();
            }
            ResponseModel rs = StaticMethods.GetLocalSavedData();
            mdl.User_id = rs.User_Id;
            PayableNotificationMdl  _payable = await api.PayableTable(mdl);
            toady_notification = new ShowPayableTodayDetail();

            ShowPaybleToday(_payable);
            ShowTotalPayble(_payable);
            var charts = CreateQuickstart(_payable);
            linechart.Chart = charts[0];
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                flag = 1;
               
               
               
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                lblparty.WidthRequest = lbloutstanding.WidthRequest = lblTodayReceipt.WidthRequest = lblCurOutstanding.WidthRequest = _Width = calcScreenWidth / 4 - 10;
            }});
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
            this.BackgroundColor = Color.FromHex("#A8C4E6");
            lblparty.BackgroundColor = lbloutstanding.BackgroundColor = lblTodayReceipt.BackgroundColor = lblCurOutstanding.BackgroundColor = lblSiteName.BackgroundColor = lblTotalDr.BackgroundColor = lblTotalCr.BackgroundColor = lblBalance.BackgroundColor = Color.FromHex("#A8C4E6");
        }

        public void ShowPaybleToday(PayableNotificationMdl _payable)
        {
            _payableshowlist = new List<ShowPayableTodayDetail>();
            try
            {
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
                        if (item3.Party_name!=null)
                        {
                            string tmp;
                            if (Convert.ToInt32(item3.Party_name.Length) >= 9)
                            {
                                tmp = item3.Party_name.Substring(0, 10);
                                tmp= tmp + "...";
                            }
                            else
                            {
                                tmp = item3.Party_name;
                            }
                            _payableshowlist.Add(new ShowPayableTodayDetail() { Show_Party_Id = item3.Party_id, txtWidth = _Width, Show_Pay_Party = tmp , Show_Pay_Outstanding = item3.Party_outstanding, Show_Amount_Received = item3.Amount_received, Show_Cur_Outstanding = item3.Current_outstanding });
                        }
                    }
                }
            }


            }
            catch (Exception ex)
            {
              //  StaticMethods.ShowToast("Internal Error Payble Today" + ex.Message);
            }
            list_today_payble.ItemsSource = _payableshowlist;
            
        }

        public void ShowTotalPayble(PayableNotificationMdl _payable)
        {
            _showpayabletotalpayblelist = new List<ShowPayableTotalPayble>();
            try
            {
            foreach (var item in _payable.ListPayablemdl)
            {
                string tmp = null;
                if (lblSiteName.Text != "Particular")
                {
                    if (Convert.ToInt32(item.Site_name.Length) >= 9)
                    {
                        tmp = item.Site_name.Substring(0, 10);
                            tmp = tmp + "...";
                    }
                    else
                    {
                        tmp = item.Site_name;
                    }
                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = tmp, Show_Balance = item.Balance, Show_Total_cr = item.Total_cr, Show_Total_dr = item.Total_dr });
                }
                else
                {

                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = item.Perticular, Show_Balance = item.Balance, Show_Total_cr = item.Receive, Show_Total_dr = item.Total_Due});
                }
            }
            }
            catch (Exception ex)
            {
             //   StaticMethods.ShowToast("Internal Error ShowTotal" + ex.Message);
            }
            list_totalpayble.ItemsSource = _showpayabletotalpayblelist;
        }

        private void list_today_payble_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            navmdl = new NavigationMdl();
            ShowPayableTodayDetail toady_notification = (ShowPayableTodayDetail)e.Item;
           
            navmdl.Party_id = toady_notification.Show_Party_Id;
            navmdl.Device_id = StaticMethods.getDeviceidentifier(); 
            if (navmdl.Device_id == "unknown")
            {
                navmdl.Device_id = "123456";
            }
            navmdl.Company_name = EnumMaster.C21_MALHAR;
            navmdl.Party_Name = toady_notification.Show_Pay_Party;
            if (this.Title== "Receivable")
            { navmdl.Tag_type = EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING; }
            else { navmdl.Tag_type = EnumMaster.TAGTYPEPAYABLE_OUTSTANDING; }
            if (Notflag != true)
            {
                Navigation.PushAsync(new PayableChart(navmdl));
            }
        }

        private async void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                isListSelected = false;
                if (e.NewTextValue != string.Empty)
                {
                    navmdl = new NavigationMdl();
                    ResponseModel rs = StaticMethods.GetLocalSavedData();
                    navmdl.User_id = rs.User_Id;
                    navmdl.Device_id = StaticMethods.getDeviceidentifier(); 
                    if (navmdl.Device_id == "unknown")
                    {
                        navmdl.Device_id = "123456";
                    }
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
                                linechart.IsVisible = false;
                            }
                            AutoList.HeightRequest = 40 * 5;
                        }
                        else
                        {
                            AutoList.ItemsSource = null;
                            AutoList.IsVisible = false;
                        }
                    });
                   
                }
                else
                {
                    AutoList.IsVisible = false;
                    linechart.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                 StaticMethods.ShowToast(ex.Message);
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
            linechart.IsVisible = true;
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
                navmdl.Device_id = StaticMethods.getDeviceidentifier(); //"123";//
                if (navmdl.Device_id == "unknown")
                {
                    navmdl.Device_id = "123456";
                }
                navmdl.Company_name = EnumMaster.C21_MALHAR;
                navmdl.Party_Name = obj.Party_Name;
                if (this.Title == "Receivable")
                { navmdl.Tag_type = EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING; }
                else { navmdl.Tag_type = EnumMaster.TAGTYPEPAYABLE_OUTSTANDING; }
                Navigation.PushAsync(new PayableChart(navmdl));
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            finally
            {
                obj = null;
                txtAuto.TextChanged += txtAuto_TextChanged;
            }
        }


        public static Chart[] CreateQuickstart(PayableNotificationMdl data)
        {
           
            var entries = new[]
            {

                            new Microcharts.Entry(100000)
                            {

                                    Label = "January",
                                    ValueLabel = "100000",
                                    Color = SKColor.Parse("#266489"), TextColor=SKColor.Parse("#EC792B"),
                            },
                            new Microcharts.Entry(250000)
                            {
                                    Label = "February",
                                    ValueLabel = "250000",
                                    Color = SKColor.Parse("#68B9C0"), TextColor=SKColor.Parse("#EC792B"),
                            },
                            new Microcharts.Entry(350000)
                            {
                                    Label = "March",
                                    ValueLabel = "350000",
                                    Color = SKColor.Parse("#90D585"),
                                    TextColor=SKColor.Parse("#EC792B"),
                            },
                        };
            
            return new Chart[]
            {
                                //new BarChart() { Entries = entries },
                                //new PointChart() { Entries = entries },
                                new LineChart() { Entries = entries ,
                                
                                                  BackgroundColor =SKColor.Parse("#A8C4E6"),
                                                  PointSize =10,
                                                  LabelTextSize =17,
                                                  LineSize =3,
                                                  PointMode =PointMode.Circle,
                                                  LineMode=LineMode.Straight},
                                //new DonutChart() { Entries = entries },
                                //new RadialGaugeChart() { Entries = entries },
                               
            };
        }
    }
   
}