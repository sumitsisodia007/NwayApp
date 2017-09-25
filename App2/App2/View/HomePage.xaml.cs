using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using App2.ShowModels;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<TempSiteIdMdl> _newTempchlst;

        public HomePage()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            //PrepareView();

            
                Task.Delay(1000);
               // SetNotificationBadge();
            });
        }
        public HomePage(LoginResponseMdl res)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            StaticMethods._new_res = _newres = res;
            PrepareView(res);
            Task.Delay(500);
             //   SetNotificationBadge();
            });
        }

        public HomePage(LoginResponseMdl res, NavigationMdl mdl)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                StaticMethods._new_res = _newres = res;
                PrepareView(res);
                Task.Delay(500);

                NavigatePageNotification(res,mdl);
            });
        }
        private async void NavigatePageNotification(LoginResponseMdl lgres, NavigationMdl nav)
        {
            ObservableCollection<SiteIdMdl> lst = new ObservableCollection<SiteIdMdl>();
            StaticMethods._new_res = _newres = lgres;
            ResponseModel res = StaticMethods.GetLocalSavedData();
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
            nav.PartyId = "1";
            if (nav.TagType == "payable_outstanding")
            {
                nav.PageTitle = LblPay.Text;
                
            }
            else if (nav.TagType == "receivable_outstanding")
            {
                nav.PageTitle = LblReceive.Text;
            }
            //nav.TagType = nav.TagType;
            await Navigation.PushAsync(new PayablePage(nav));
        }

        public HomePage(LoginResponseMdl res, List<TempSiteIdMdl> tempchlst)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                StaticMethods._new_res = _newres = res;
                _newTempchlst = tempchlst;
                PrepareView(res);
                Task.Delay(500);
                //   SetNotificationBadge();
            });
        }


        //private async void SetNotificationBadge()
        //{
        //    try
        //    {
        //        string dateChk = null;
        //        string notcount = "0";
        //        ResponseModel rs = StaticMethods.GetLocalSavedData();
        //        _api = new API();
        //        NavigationMdl nav = new NavigationMdl();
        //        nav.DeviceId = StaticMethods.GetDeviceidentifier();
        //        if (nav.DeviceId == "unknown")
        //        {
        //            nav.DeviceId = "123456";
        //        }
        //        nav.CompanyName = EnumMaster.C21Malhar;
        //        nav.TagType = EnumMaster.Tagtypenotifications;

        //        nav.UserId = rs.UserId;
        //       // _notificationModel = api.PostNotification(nav);


        //        var d2 = DateTime.Now.ToString("dd-MMM-yyyy");
        //        foreach (var item in _notificationModel.ListNotificationDate)
        //        {
        //            dateChk = item.Date;
        //            notcount = item.NotCount;
        //            break;
        //        }
        //        if (d2.ToString() == dateChk)
        //        {
        //            rs.NotCount = LblNotificationBadge.Text = notcount;
        //        }
        //        else
        //        {
        //            LblNotificationBadge.Text = "0";
        //            //rs.NotCountDate = DateChk;
        //            //StaticMethods.SaveLocalData(rs);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //  await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
        //    }
        //}
        
        private async void Receivable_Tapped(object sender, EventArgs e)
        {
            //ResponseModel res = new ResponseModel();
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
                _objNav = new NavigationMdl();
                NavigationMdl nav = _objNav.PrepareApiData();
                // PayableNotificationMdl _payable = await api.PayableTable(nav);
                nav.PageTitle = LblReceive.Text;
                nav.TagType = EnumMaster.TagtypereceivableOutstanding;
                await Task.Delay(200);
                await Navigation.PushAsync(new PayablePage(nav));

            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            
        }

        private async void Payable_Tapped(object sender, EventArgs e)
        {
            _objNav = new NavigationMdl();
            NavigationMdl nav = _objNav.PrepareApiData();
            nav.PageTitle = LblPay.Text;
            
            nav.TagType = EnumMaster.TagtypepayableOutstanding;
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await Navigation.PushAsync(new PayablePage(nav));
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private async void CashFlow_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CashFlowPage());
        }

        private async void Elect_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ele_CunsPage());
        }

        private void Expired_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExpiredSoon());
        }

        private void Canceled_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
        }

        private async void Notification_Clicked(object sender, EventArgs e)
        {
            try
            {
                //var loadingPage = new LoaderPage();
                //await PopupNavigation.PushAsync(loadingPage);
                //await Task.Delay(1000);

                //await Navigation.PushAsync(new MainPage(_notificationModel));
                //await Navigation.RemovePopupPageAsync(loadingPage);
                _objNav = new NavigationMdl();
                NavigationMdl nav = _objNav.PrepareApiData();
                NotificationListMdl nmdl = null;
                nmdl = _newTempchlst == null ? _api.PostNotification(nav) : _api.PostNotification(nav, _newTempchlst);
                  
                
                if (nmdl.Error == "true")
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", nmdl.Message));
                }
                else
                {
                    var loadingPage = new LoaderPage();
                    await PopupNavigation.PushAsync(loadingPage);
                    await Task.Delay(500);
                    await Navigation.PushAsync(new MainPage(nmdl));
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }
            }
            catch (Exception ex)
            {
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

        private void Alert_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon, Alert", "ok");
        }

        private async void Approval_Clicked(object sender, EventArgs e)
        {
           await  DisplayAlert("Message", "Comming Soon, Approval", "ok");
        }

        //private NavigationMdl PrepareAPIData()
        //{
        //    NavigationMdl nav = new NavigationMdl();
        //    ObservableCollection<SiteIdMdl> lst = new ObservableCollection<SiteIdMdl>();
        //    ResponseModel res = StaticMethods.GetLocalSavedData();
        //    try
        //    {
        //        foreach (var item in _newres._permissions)
        //        {
        //            if (StaticMethods.SetCompanyName == item.CompanyName)
        //            {
        //                foreach (var item2 in item.Sites)
        //                {

        //                    lst.Add(new SiteIdMdl { SiteId = item2.SiteId, SiteName = item2.Site_name });
        //                }
        //                nav.CompanyId = item.Company_id.ToString();
        //            }
        //        }
        //        nav.UserName = res.UserName;
        //        nav.Password = res.Password;
        //        nav.DeviceId = res.DeviceId;
        //        nav.UserId = res.UserId;
        //        nav.TagType = EnumMaster.Tagtypenotifications;
        //        nav.SiteIdMdls = lst;
        //        nav.PartyId = "1";
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return nav;
        //}

        private void PrepareView(LoginResponseMdl res)
        {
            try
            {
                ResponseModel res1 = StaticMethods.GetLocalSavedData();
                if (res1.CompanyName != null)
                {
                    LblSetComName.Text = StaticMethods.SetCompanyName=res1.CompanyName;
                }
                else
                {
                    foreach (var item in res._permissions)
                    {
                        StaticMethods.SetCompanyName= LblSetComName.Text = item.CompanyName;
                        break;
                    }
                }
                if (App.ScreenWidth > 0 && App.ScreenHeight > 0)
                {
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
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "ok");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new LeftMenu(_newres));
        }
    }
}
