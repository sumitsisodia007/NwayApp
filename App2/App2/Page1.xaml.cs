using App2.APIService;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private NavigationMdl _objNav = null;
        private LoginMdl _login = new LoginMdl();
        private readonly API _api = new API();

        private LoginResponseMdl _newres;

        public Page1()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            _newres = StaticMethods.NewRes;
            PrepareMainData(StaticMethods.NewRes);
        }
        public Page1(LoginResponseMdl res)
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            StaticMethods.NewRes = _newres = res;
            PrepareMainData(StaticMethods.NewRes);
        }
       
        public Page1(LoginResponseMdl res, NavigationMdl mdl)
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            StaticMethods.NewRes = _newres = res;
            var rs = StaticMethods.GetLocalSavedData();
            LblNotificationBadge.Text = rs.NotCount;
            
            NavigatePageNotification(res, mdl);
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
               // await Navigation.PushAsync(new PayablePage(nav));
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast("Error NavigatePage via Not Method(Home)" + ex.Message);
            }
        }
        #endregion

        private void MainManuSlide_Tapped(object sender, EventArgs e)
        {
            if ((App.MasterDetail.IsPresented) == false)
            {
                App.MasterDetail.IsPresented = true;
            }
        }
        private void Notification_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "Notification_Tapped", "ok");
        }
        private void Alert_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "Alert_Tapped", "ok");
        }
        private void Approvel_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "Approvel_Tapped", "ok");
        }

        private async void SetCompany_Tapped(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            StaticMethods.SetCompanyName = LblSetComName.Text;
            await PopupNavigation.PushAsync(new LeftMenu(_newres));
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private void Receivable_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "Receivable_Tapped", "ok");
        }
        private void Payable_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "Payable_Tapped", "ok");
        }
        private void Bank_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "Bank_Tapped", "ok");
        }
        private void ElectricityTapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "ElectricityTapped", "ok");
        }
        private void ExpiredSoon_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "ExpiredSoon_Tapped", "ok");
        }
        private void InvoiceCancel_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Notify", "InvoiceCancel_Tapped", "ok");
        }

       async Task PrepareMainData(LoginResponseMdl res)
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

            await Task.Delay(500);
            foreach (var item in StaticMethods.StaticHome.ListHomeDetails)
            {
                LblElecReading.Text = item.electric_consumption;
                LblBankAmt.Text = item.bank;
                LblReceiveble.Text = item.receivable;
                LblPayable.Text = item.payable;
                LblExpire.Text = item.expire.ToString();
                LblInvoiceCancel.Text = item.cancellation.ToString();
                LblNotificationBadge.Text = item.notificationCount.ToString();
            }
        }
    }
}