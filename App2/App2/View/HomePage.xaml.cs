using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using App2.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private NavigationMdl _objNav = null;
        private LoginMdl _login = new LoginMdl();
        private readonly API _api = new API();

        private LoginResponseMdl _newres;
        //NotificationListMdl _notificationModel;
        //private List<TempSiteIdMdl> _newTempchlst;

        public HomePage()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                //PrepareView();
                Task.Delay(500);
                UserModel rs = StaticMethods.GetLocalSavedData();
                LblNotificationBadge.Text = rs.NotCount;
                LblSetComName.Text = rs.CompanyName;
                PrepareView(StaticMethods.NewRes);
                Task.Delay(500);
                PrepareHomePage();
                //PrepareBankAmount();
                //PrepareMeterReading();
            });
        }

        public HomePage(LoginResponseMdl res)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                StaticMethods.NewRes = _newres = res;
                PrepareView(res);
                Task.Delay(500);
                PrepareHomePage();
                //PrepareBankAmount();
                //PrepareMeterReading();
            });
        }
        
        public HomePage(LoginResponseMdl res, NavigationMdl mdl)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                StaticMethods.NewRes = _newres = res;
                PrepareView(res);
                UserModel rs = StaticMethods.GetLocalSavedData();
                LblNotificationBadge.Text = rs.NotCount;

                NavigatePageNotification(res, mdl);
            });
        }

        //public HomePage(LoginResponseMdl res, List<TempSiteIdMdl> tempchlst)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        InitializeComponent();
        //        Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        //        StaticMethods.NewRes = _newres = res;
        //        _newTempchlst = tempchlst;
        //        PrepareView(res);
        //        var rs = StaticMethods.GetLocalSavedData();
        //        LblNotificationBadge.Text = rs.NotCount;
        //        PrepareBankAmount();
        //        PrepareMeterReading();
        //    });
        //}



        protected override void OnAppearing()
        {
            HidePopup();
        }

        private async void HidePopup()
        {
            try
            {
                var rs = StaticMethods.GetLocalSavedData();
                //if (rs.DeviceToken == null)
                //{
                await Task.Delay(3000);
                await MainTokanReg();
                //}
            }
            catch (Exception ex)
            {
               // StaticMethods.ShowToast("Error HidePopup Method(Home)" + ex.Message);
            }
        }

        private Task<int> MainTokanReg()
        {
            try
            {
               // int unique = 1;
                LoginResponseMdl login;
                var lgnmdl = new LoginMdl();

                var userModel = StaticMethods.GetLocalSavedData();
                lgnmdl.Username = userModel.UserName;
                lgnmdl.Password = userModel.Password;
                lgnmdl.Tagtype = EnumMaster.TagtypeSignin;
                lgnmdl.DeviceId = StaticMethods.GetDeviceidentifier();
                if (lgnmdl.DeviceId == "unknown")
                {
                    lgnmdl.DeviceId = "123456";
                }
                if (Device.OS == TargetPlatform.iOS)
                {
                    lgnmdl.IosToken = DependencyService.Get<IIosMethods>().GetTokan();
                    userModel.DeviceToken = lgnmdl.IosToken;
                }
                else
                {
                    lgnmdl.Firebasetoken = DependencyService.Get<IAndroidMethods>().GetTokan() ?? "";
                }
                login = _api.PostLogin(lgnmdl);


                if (login.Error == "false")
                {
                    //_newres = res;
                    StaticMethods.NewRes = _newres = login;
                    if (userModel.DeviceToken == null)
                    {

                        StaticMethods.SaveLocalData(userModel);
                    }
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("Error MainTokanReg Method(Home)" + ex.Message);
            }
            return null;
        }

        #region Via Notification
        private async void NavigatePageNotification(LoginResponseMdl lgres, NavigationMdl nav)
        {
            try
            {
                ObservableCollection<SiteIdMdl> lst = new ObservableCollection<SiteIdMdl>();
                StaticMethods.NewRes = _newres = lgres;
                UserModel res = StaticMethods.GetLocalSavedData();
                foreach (var item in lgres._permissions)
                {
                    if (StaticMethods.SetCompanyName == item.CompanyName)
                    {
                        foreach (var item2 in item.Sites)
                        {

                            lst.Add(new SiteIdMdl { SiteId = item2.Site_id, SiteName = item2.Site_name });
                        }
                        nav.CompanyId = item.CompanyId.ToString();
                    }
                }
                nav.UserName = res.UserName;
                nav.Password = res.Password;
                nav.DeviceId = res.DeviceId;
                nav.UserId = res.UserId;
                nav.SiteIdMdls = lst;

                switch (nav.TagType)
                {
                    case "payable_outstanding":
                        nav.PageTitle = LblPay.Text;
                        break;
                    case "receivable_outstanding":
                        nav.PageTitle = LblReceive.Text;
                        break;
                }
                //nav.TagType = nav.TagType;
                await Navigation.PushAsync(new PayablePage(nav));

            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("Error NavigatePage via Not Method(Home)" + ex.Message);
            }
        }
        #endregion
      
        private async void Receivable_Tapped(object sender, EventArgs e)
        {
            //UserModel res = new UserModel();
            //nav = new NavigationMdl();
            //nav.PageTitle = LblReceive.Text;
            //nav.UserId = res.UserId;
            //nav.DeviceId = res.DeviceId;
            //if (nav.DeviceId == "unknown")
            //{
            //    nav.DeviceId = "123456";
            //}
            //nav.CompanyName = res.CompanyName;
            try
            {
                var loadingPage = new LoaderPage();
                await PopupNavigation.PushAsync(loadingPage);
                _objNav = new NavigationMdl();
                NavigationMdl nav = _objNav.PrepareApiData();
                // PayableNotificationMdl _payable = await api.PayableTable(nav);
                nav.PageTitle = LblReceive.Text;
                nav.TagType = EnumMaster.TagtypereceivableOutstanding;
                await Task.Delay(200);
                await Navigation.PushAsync(new PayablePage(nav));
                await PopupNavigation.RemovePageAsync(loadingPage);

            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("Error Receivable_Tapped Method(Home)" + ex.Message);
            }

        }

        private async void Payable_Tapped(object sender, EventArgs e)
        {
            try
            {
                var loadingPage = new LoaderPage();
                await PopupNavigation.PushAsync(loadingPage);
                _objNav = new NavigationMdl();
                NavigationMdl nav = _objNav.PrepareApiData();
                nav.PageTitle = LblPay.Text;
                nav.TagType = EnumMaster.TagtypepayableOutstanding;
                await Navigation.PushAsync(new PayablePage(nav));
                await PopupNavigation.RemovePageAsync(loadingPage);

            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("Error Payable_Tapped Method(Home)" + ex.Message);
            }
        }

        private async void CashFlow_Tapped(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            _objNav = new NavigationMdl();
            NavigationMdl nav = _objNav.PrepareApiData();
            var cashdetails = _api.CashFlowDetails(nav);
            if (cashdetails.Error == "false")
            {
                StaticMethods.BankRes = cashdetails;
            }
            await Navigation.PushAsync(new CashFlowSitePage());
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private async void Elect_Tapped(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            _objNav = new NavigationMdl();
            NavigationMdl nav = _objNav.PrepareApiData();
            var electrCons = _api.ElectricityCuns(nav);
            if (electrCons.Error == false)
            {
                StaticMethods.ElectricityResp = electrCons;
            }
            await Navigation.PushAsync(new Ele_CunsPageCont());
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private async void Expired_Tapped(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await Navigation.PushAsync(new ExpiredSoon());
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private async void Canceled_Tapped(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await DisplayAlert("Message", "Comming Soon", "ok");
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private async void Notification_Clicked(object sender, EventArgs e)
        {
            try
            {
                var loadingPage = new LoaderPage();
                await PopupNavigation.PushAsync(loadingPage);
                _objNav = new NavigationMdl();
                NavigationMdl nav = _objNav.PrepareApiData();
                NotificationListMdl nmdl = null;

                nmdl = _api.PostNotification(nav);
                if (nmdl.Error == "true")
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", nmdl.Message));
                }
                else
                {

                    UserModel rs = StaticMethods.GetLocalSavedData();
                    var d2 = DateTime.Now.ToString("dd-MMM-yyyy");
                    string dateChk = null;
                    string notcount = "0";

                    foreach (var item in nmdl.ListNotificationDate)
                    {
                        dateChk = item.Date;
                        notcount = item.NotCount;
                        break;
                    }
                    rs.NotCount = d2.ToString() == dateChk ? notcount : "0";
                    LblNotificationBadge.Text = rs.NotCount;
                    StaticMethods.SaveLocalData(rs);
                    await Navigation.PushAsync(new MainPage(nmdl));
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("Error Notification_Clicked Method(Home)" + ex.Message);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void MainManuSlide_Tapped(object sender, EventArgs e)
        {
            if ((App.MasterDetail.IsPresented) == false)
            {
                App.MasterDetail.IsPresented = true;
            }
        }

        private async void Alert_Tapped(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await DisplayAlert("Message", "Comming Soon, Alert", "ok");
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private async void Approval_Clicked(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await DisplayAlert("Message", "Comming Soon, Approval", "ok");
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private void PrepareView(LoginResponseMdl res)
        {
            try
            {
                var res1 = StaticMethods.GetLocalSavedData();
                if (res1.CompanyName != null)
                {
                    LblSetComName.Text = StaticMethods.SetCompanyName = res1.CompanyName;
                }
                else
                {
                    foreach (var item in res._permissions)
                    {
                        res1.CompanyName = StaticMethods.SetCompanyName = LblSetComName.Text = item.CompanyName;
                        res1.CompanyIndex = item.CompanyId.ToString();
                        break;
                    }
                }
                StaticMethods.SaveLocalData(res1);
                if (!(App.ScreenWidth > 0) || !(App.ScreenHeight > 0)) return;
                var calcScreenWidth = App.ScreenWidth;
                var calcScreenHieght = App.ScreenHeight;
                GridRec.HeightRequest =
                    GridPay.HeightRequest =
                        GridCas.HeightRequest =
                            GridCon.HeightRequest =
                                GridExp.HeightRequest =
                                    GridInv.HeightRequest = calcScreenHieght / 4 + 20;
                GridRec.WidthRequest =
                    GridPay.WidthRequest =
                        GridCas.WidthRequest =
                            GridCon.WidthRequest =
                                GridExp.WidthRequest =
                                    GridInv.WidthRequest = calcScreenWidth / 3;
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("Error PrepareView Method(Home)" + ex.Message);
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            StaticMethods.SetCompanyName = LblSetComName.Text;
            await PopupNavigation.PushAsync(new LeftMenu(_newres));
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private void PrepareBankAmount()
        {
            try
            {
                var rs = StaticMethods.GetLocalSavedData();
                var cash = StaticMethods.BankRes;
                foreach (var items in cash.ListCashFlowSite)
                {
                    if (rs.CompanyName == items.CompanyName)
                    {
                        LblBankAmt.Text = items.Amt + " " + items.AmtType;
                    }
                }
                LblNotificationBadge.Text = rs.NotCount;
            }
            catch (Exception e)
            {
                StaticMethods.ShowToast("Error PrepareBankAmount Method(Home)" + e.Message);
            }
        }

        private void PrepareMeterReading()
        {
            var cash = StaticMethods.ElectricityResp;
            var ss = cash.ListElectricityGroupMdl.MpebTotalConsumption.ToString() + "/ " +
                        cash.ListElectricityGroupMdl.OtherTotalConsumption.ToString();

            LblElecReading.Text = ss; //rs.NotCount;
        }

        private void PrepareHomePage()
        {
            //var hamedata = StaticMethods.StaticHome;
            var rs = StaticMethods.GetLocalSavedData();
            LblNotificationBadge.Text = rs.NotCount;
            foreach (var item in StaticMethods.StaticHome.ListHomeDetails)
            {
                LblElecReading.Text = item.electric_consumption;
                LblBankAmt.Text = item.bank;
                LblReceiveble.Text = item.receivable;
                LblPayable.Text = item.payable;
                LblExpire.Text = item.expire.ToString();
                LblInvoiceCancel.Text = item.cancellation.ToString();
            }
        }
    }
}
