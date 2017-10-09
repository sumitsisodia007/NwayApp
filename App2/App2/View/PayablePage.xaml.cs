using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using App2.ShowModels;
using Microcharts;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayablePage : ContentPage
    {
        public List<ShowPayableTodayDetail> Payableshowlist { get; set; }
        public List<ShowPayableTotalPayble> Showpayabletotalpayblelist { get; set; }
        public static SKColor ColorsPayable = SKColors.Gray;
        NavigationMdl _objNav = null;
        public bool Notflag { get; set; }
        PartysearchMdl _lstLoca = null;
        bool _isListSelected = false;
        ShowPayableTodayDetail _toadyNotification;
        NavigationMdl _navmdl = new NavigationMdl();

        public double _Width = 0;
        public static int Flag = 0;
        API _api = new API();

        public PayablePage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);

        }

        public PayablePage(NavigationMdl mdl)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            //NavigationPage.SetTitleIcon(this, "icon.png");
            try
            {
                this.Title = mdl.PageTitle;
                MAinMethods(mdl);
                Flag = 1;
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }

            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    Notflag = mdl.IsNotification;


            //    navmdl = new NavigationMdl();
            //    if (mdl.PageTitle == "Receivable")
            //    {
            //        PredefinedReceived();
            //    }
            //    else
            //    {
            //        PredefinedPaid();
            //    }
            //    UserModel rs = StaticMethods.GetLocalSavedData();
            //    mdl.UserId = rs.UserId;
            //    PayableNotificationMdl _payable = await api.PayableTable(mdl);

            //    toady_notification = new ShowPayableTodayDetail();
            //    flag = 1;

            //    ShowPaybleToday(_payable);
            //    ShowTotalPayble(_payable);
            //});
        }

        private void MAinMethods(NavigationMdl mdl)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var loadingPage = new LoaderPage();
                    await PopupNavigation.PushAsync(loadingPage);

                    if (mdl.PageTitle == "Receivable")
                    {
                        PredefinedReceived();
                    }
                    else
                    {
                        PredefinedPaid();
                    }
                    if (!CrossConnectivity.Current.IsConnected)
                    {

                        await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
                    }
                    else
                    {
                        PayableNotificationMdl payable = await _api.PayableTable(mdl);

                        UserModel rs = StaticMethods.GetLocalSavedData();
                        mdl.UserId = rs.UserId;

                        _toadyNotification = new ShowPayableTodayDetail();

                        ShowPaybleToday(payable);
                        ShowTotalPayble(payable);
                    }
                    await PopupNavigation.RemovePageAsync(loadingPage);
                });

            }
            catch (Exception ex)
            {
            }

        }

        protected override void OnAppearing()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Flag = 1;
                    if (!(Application.Current.MainPage.Width > 0) || !(Application.Current.MainPage.Height > 0)) return;
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;
                    Lblparty.WidthRequest = Lbloutstanding.WidthRequest = LblTodayReceipt.WidthRequest = LblCurOutstanding.WidthRequest = _Width = calcScreenWidth / 4 - 10;
                });
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("OnApp Payable " + ex.Message);
            }

        }

        private void PredefinedPaid()
        {
            LblFirstTitle.Text = EnumMaster.LblPaidFirstTitle;
            LblSecoundTitle.Text = EnumMaster.LblPaidSecoundTitle;
            Lblparty.Text = EnumMaster.LblPaidReceivedParty;
            Lbloutstanding.Text = EnumMaster.LblPaidOutstanding;
            LblTodayReceipt.Text = EnumMaster.LblPaidTodayPaid;
            LblCurOutstanding.Text = EnumMaster.LblPaidCurOutstanding;
            LblSiteName.Text = EnumMaster.LblPaidSiteName;
            LblTotalDr.Text = EnumMaster.LblPaidTotaleDr;
            LblTotalCr.Text = EnumMaster.LblPaidTotaleCr;
            LblBalance.Text = EnumMaster.LblPaidReceivedBalance;
            this.BackgroundColor = Color.FromHex("#ED7D31");
            ColorsPayable = SKColor.Parse("#ED7D31");
            Lblparty.BackgroundColor = Lbloutstanding.BackgroundColor = LblTodayReceipt.BackgroundColor = LblCurOutstanding.BackgroundColor = LblSiteName.BackgroundColor = LblTotalDr.BackgroundColor = LblTotalCr.BackgroundColor = LblBalance.BackgroundColor = Color.FromHex("#ED7D31");
        }

        private void PredefinedReceived()
        {
            LblFirstTitle.Text = EnumMaster.LblReceivedFirstTitle;
            LblSecoundTitle.Text = EnumMaster.LblReceivedSecoundTitle;
            Lblparty.Text = EnumMaster.LblPaidReceivedParty;
            Lbloutstanding.Text = EnumMaster.LblReceivedOutstanding;
            LblTodayReceipt.Text = EnumMaster.LblReceivedTodayPaid;
            LblCurOutstanding.Text = EnumMaster.LblReceivedCurOutstanding;
            LblSiteName.Text = EnumMaster.LblReceivedSiteName;
            LblTotalDr.Text = EnumMaster.LblReceivedTotaleDr;
            LblTotalCr.Text = EnumMaster.LblReceivedTotaleCr;
            LblBalance.Text = EnumMaster.LblPaidReceivedBalance;
            this.BackgroundColor = Color.FromHex("#A8C4E6");
            ColorsPayable = SKColor.Parse("#A8C4E6");
            Lblparty.BackgroundColor = Lbloutstanding.BackgroundColor = LblTodayReceipt.BackgroundColor = LblCurOutstanding.BackgroundColor = LblSiteName.BackgroundColor = LblTotalDr.BackgroundColor = LblTotalCr.BackgroundColor = LblBalance.BackgroundColor = Color.FromHex("#A8C4E6");
        }

        public void ShowPaybleToday(PayableNotificationMdl payable)
        {
            Payableshowlist = new List<ShowPayableTodayDetail>();
            try
            {
                foreach (var item in payable.ListPayableNotification)
                {
                    foreach (var item2 in item.ListPayablemdl)
                    {
                        //Total Payble List Show
                        Stktodaypayable.IsVisible = item2.Notification != null;
                        foreach (var item3 in item2.Notification)
                        {
                            if (item3.Party_name == null) continue;
                            string tmp;
                            if (Convert.ToInt32(item3.Party_name.Length) >= 9)
                            {
                                tmp = item3.Party_name.Substring(0, 10);
                                tmp = tmp + "...";
                            }
                            else
                            {
                                tmp = item3.Party_name;
                            }
                            Payableshowlist.Add(new ShowPayableTodayDetail() { ShowPartyId = item3.Party_id, TxtWidth = _Width, ShowPayParty = tmp, ShowPayOutstanding = item3.Party_outstanding, ShowAmountReceived = item3.Amount_received, ShowCurOutstanding = item3.Current_outstanding });
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                //  StaticMethods.ShowToast("Internal Error Payble Today" + ex.Message);
            }
            PaybleItemSource.ItemsSource = Payableshowlist;

        }

        public void ShowTotalPayble(PayableNotificationMdl payable)
        {
            Showpayabletotalpayblelist = new List<ShowPayableTotalPayble>();
            try
            {
                foreach (var item in payable.ListPayablemdl)
                {
                    if (LblSiteName.Text != "Particular")
                    {
                        string tmp = null;
                        if (Convert.ToInt32(item.Site_name.Length) >= 9)
                        {
                            tmp = item.Site_name.Substring(0, 10);
                            tmp = tmp + "...";
                        }
                        else
                        {
                            tmp = item.Site_name;
                        }
                        Showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { TxtWidth = _Width, ShowSiteName = tmp, ShowBalance = item.Balance, ShowTotalCr = item.Total_cr, ShowTotalDr = item.Total_dr });
                    }
                    else
                    {

                        Showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { TxtWidth = _Width, ShowSiteName = item.Perticular, ShowBalance = item.Balance, ShowTotalCr = item.Receive, ShowTotalDr = item.Total_Due });
                    }
                }

                if (LblSiteName.Text != "Particular")
                {
                    var charts = CreateQuickstart1(Showpayabletotalpayblelist);
                    Linechart.Chart = charts[0];
                }
                else
                {
                    var charts = CreateQuickstart(Showpayabletotalpayblelist);
                    Linechart.Chart = charts[0];
                }
            }
            catch (Exception ex)
            {
               // StaticMethods.ShowToast("Internal Error ShowTotal" + ex.Message);
            }
            list_totalpayble.ItemsSource = Showpayabletotalpayblelist;
        }

        private void list_today_payble_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _navmdl = new NavigationMdl();
            var localToadyNotification = (ShowPayableTodayDetail)e.Item;

            //navmdl.DeviceId = StaticMethods.GetDeviceidentifier(); 
            //if (navmdl.DeviceId == "unknown")
            //{
            //    navmdl.DeviceId = "123456";
            //}
            //navmdl.CompanyName = EnumMaster.C21Malhar;
            _objNav = new NavigationMdl();
            NavigationMdl nav = _objNav.PrepareApiData();
            nav.PartyId = localToadyNotification.ShowPartyId;
            nav.PartyName = localToadyNotification.ShowPayParty;
            nav.TagType = this.Title == "Receivable" ? EnumMaster.TagtypereceivableOutstanding : EnumMaster.TagtypepayableOutstanding;
            if (Notflag != true)
            {
                Navigation.PushAsync(new PayableChart(nav));
            }
        }

        private async void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _isListSelected = false;
                if (e.NewTextValue != string.Empty)
                {
                    //navmdl = new NavigationMdl();
                    //UserModel rs = StaticMethods.GetLocalSavedData();
                    //navmdl.UserId = rs.UserId;
                    //navmdl.DeviceId = StaticMethods.GetDeviceidentifier(); 
                    //if (navmdl.DeviceId == "unknown")
                    //{
                    //    navmdl.DeviceId = "123456";
                    //}
                    //navmdl.CompanyName = Helper.EnumMaster.C21Malhar;
                    //navmdl.PartyName = e.NewTextValue;
                    //navmdl.TagType = "partylist";

                    ObservableCollection<PartysearchlistMdl> lst = new ObservableCollection<PartysearchlistMdl>();
                    _api = new API();
                    if (!CrossConnectivity.Current.IsConnected)
                    {

                        await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
                    }
                    else
                    {
                        _objNav = new NavigationMdl();
                        NavigationMdl nav = _objNav.PrepareApiData();
                        nav.PartyName = e.NewTextValue;
                        _lstLoca = new PartysearchMdl();
                        _lstLoca = await _api.GetParty(nav);
                        if (_lstLoca.Error != "true")
                        {
                            foreach (var item in _lstLoca.Party_List)
                            {
                                lst.Add(new PartysearchlistMdl
                                {
                                    Party_Id = item.Party_Id,
                                    Party_Name = item.Party_Name
                                });
                            }
                            AutoList.ItemsSource = lst;
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (lst.Count > 0)
                                {
                                    AutoList.IsVisible = true;
                                    if (AutoList.IsVisible == true)
                                    {
                                        Linechart.IsVisible = false;
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
                            await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", _lstLoca.Message));
                        }
                    }
                }
                else
                {
                    AutoList.IsVisible = false;
                    Linechart.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
        }

        private async void txtAuto_Focused(object sender, FocusEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {
                await Task.Delay(100);
            }
        }

        async void txtLocation_Focus(object sender, EventArgs args)
        {
            await Task.Delay(2000);
            txtAuto.Unfocus();
            txtAuto.Focus();
        }

        private void txtAuto_Unfocused(object sender, FocusEventArgs e)
        {
            if (!_isListSelected)
            {
                txtAuto.TextChanged -= txtAuto_TextChanged;
                txtAuto.Text = string.Empty;
                txtAuto.TextChanged += txtAuto_TextChanged;
            }

            AutoList.IsVisible = false;
            Linechart.IsVisible = true;
            txtAuto.Placeholder = "Select Party";

        }

        private void AutoList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            PartysearchlistMdl obj = null;
            _navmdl = new NavigationMdl();
            try
            {
                _isListSelected = true;
                AutoList.IsVisible = false;
                txtAuto.TextChanged -= txtAuto_TextChanged;
                txtAuto.Unfocus();
                obj = (PartysearchlistMdl)e.Item;

                //navmdl.DeviceId = StaticMethods.GetDeviceidentifier(); //"123";//
                //if (navmdl.DeviceId == "unknown")
                //{
                //    navmdl.DeviceId = "123456";
                //}
                //navmdl.CompanyName = EnumMaster.C21Malhar;
                _objNav = new NavigationMdl();
                NavigationMdl nav = _objNav.PrepareApiData();
                nav.PartyName = obj.Party_Name;
                nav.PartyId = obj.Party_Id;
                nav.TagType = this.Title == "Receivable" ? EnumMaster.TagtypereceivableOutstanding : EnumMaster.TagtypepayableOutstanding;
                Navigation.PushAsync(new PayableChart(nav));
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

        public static Chart[] CreateQuickstart(List<ShowPayableTotalPayble> data)
        {

            //List<ShowPayableTotalPayble>  _showlist = new List<ShowPayableTotalPayble>();
            //foreach (var item in data.ListPayablemdl)
            //{

            //        _showlist.Add(new ShowPayableTotalPayble() { Show_Site_name = item.Perticular, Show_Balance = item.Balance, Show_Total_cr = item.Receive, Show_Total_dr = item.Total_Due });
            //}
            var a1 = Convert.ToSingle(data[0].ShowBalance);
            var b1 = Convert.ToSingle(data[1].ShowBalance);
            var c1 = Convert.ToSingle(data[2].ShowBalance);
            var d1 = Convert.ToSingle(data[3].ShowBalance);

            var entries = new[]
            {

                            new Microcharts.Entry(a1)
                            {

                                    Label = data[0].ShowSiteName,
                                    ValueLabel = a1.ToString(),
                                    Color = SKColor.Parse("#266489"), TextColor=SKColor.Parse("#EC792B"),
                            },
                            new Microcharts.Entry(b1)
                            {
                                    Label = data[1].ShowSiteName,
                                    ValueLabel = b1.ToString(),
                                    Color = SKColor.Parse("#68B9C0"), TextColor=SKColor.Parse("#EC792B"),
                            },
                            new Microcharts.Entry(c1)
                            {
                                    Label = data[2].ShowSiteName,
                                    ValueLabel = c1.ToString(),
                                    Color = SKColor.Parse("#90D585"),
                                    TextColor=SKColor.Parse("#EC792B"),
                            },
                            new Microcharts.Entry(d1)
                            {
                                    Label = data[3].ShowSiteName,
                                    ValueLabel = d1.ToString(),
                                    Color = SKColor.Parse("#90D585"),
                                    TextColor=SKColor.Parse("#EC792B"),
                            },
                        };

            return new Chart[]
            {
                                //new BarChart() { Entries = entries },
                                //new PointChart() { Entries = entries },
                                new LineChart() { Entries = entries ,

                                                  BackgroundColor =ColorsPayable,
                                                  PointSize =10,
                                                  LabelTextSize =17,
                                                  LineSize =3,
                                                  PointMode =PointMode.Circle,
                                                  LineMode=LineMode.Straight},
                                //new DonutChart() { Entries = entries },
                                //new RadialGaugeChart() { Entries = entries },
                               
            };
        }

        public static Chart[] CreateQuickstart1(List<ShowPayableTotalPayble> data)
        {
            ColorsPayable = SKColor.Parse("#EC792B");

            var a1 = Convert.ToSingle(100);
            var b1 = Convert.ToSingle(200);
            var c1 = Convert.ToSingle(300);
            var entries = new[]
            {
                            new Microcharts.Entry(a1)
                            {
                                    Label = "ShowSiteName 1",//data[0].ShowSiteName,
                                    ValueLabel = a1.ToString(),
                                    Color = SKColor.Parse("#266489"), TextColor=SKColor.Parse("#2A83C6"),
                            },
                            new Microcharts.Entry(b1)
                            {
                                    Label ="ShowSiteName 2",// data[1].ShowSiteName,
                                    ValueLabel = b1.ToString(),
                                    Color = SKColor.Parse("#68B9C0"), TextColor=SKColor.Parse("#2A83C6"),
                            },
                            new Microcharts.Entry(c1)
                            {
                                    Label = "ShowSiteName 3",//data[2].ShowSiteName,
                                    ValueLabel = c1.ToString(),
                                    Color = SKColor.Parse("#90D585"),
                                    TextColor=SKColor.Parse("#2A83C6"),
                            },
                        };
            return new Chart[]
            {
                                new LineChart() { Entries = entries ,
                                                  BackgroundColor =ColorsPayable,
                                                  PointSize =10,
                                                  LabelTextSize =17,
                                                  LineSize =3,
                                                  PointMode =PointMode.Circle,
                                                  LineMode=LineMode.Straight},
            };
        }
    }

}