using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
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
        NavigationMdl nav = null;
        LoginMdl _login = new LoginMdl();
        API api = new API();
        LoginResponseMdl _newres;
        NotificationListMdl _notificationModel;
        public HomePage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            PrepareView();
            Device.BeginInvokeOnMainThread(() => {
                Task.Delay(1000);
                SetNotificationBadge();
            });
        }
        public HomePage(LoginResponseMdl res)
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            _newres = res;
            PrepareView();
            Device.BeginInvokeOnMainThread(() => {
                Task.Delay(500);
                SetNotificationBadge();
           });
        }

        private async void SetNotificationBadge()
        {
             try
            {
            string DateChk = null;
            string Notcount = "0";
            ResponseModel rs = StaticMethods.GetLocalSavedData();
            api = new API();
            NavigationMdl nav = new NavigationMdl();
            nav.Device_id = StaticMethods.getDeviceidentifier();
            if (nav.Device_id == "unknown")
            {
                nav.Device_id = "123456";
            }
            nav.Company_name = EnumMaster.C21_MALHAR;
            nav.Tag_type = EnumMaster.TAGTYPENOTIFICATIONS;

            nav.User_id = rs.User_Id;
            _notificationModel = api.PostNotification(nav);
            

            var d2 = DateTime.Now.ToString("dd-MMM-yyyy");
            foreach (var item in _notificationModel.ListNotificationDate)
            {
                DateChk= item.Date;
                Notcount= item.NotCount;
                break;
            }
            if (d2.ToString() == DateChk)
            {
                rs.NotCount = lblNotificationBadge.Text = Notcount;
            }
            else
            {
                lblNotificationBadge.Text = "0";
                        //rs.NotCountDate = DateChk;
                        //StaticMethods.SaveLocalData(rs);
                    }
                }
            catch (Exception ex)
            {
             //  await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
        }


        private async void Receivable_Tapped(object sender, EventArgs e)
        {
            nav = new NavigationMdl();
            nav.Page_Title = lblReceive.Text;
            nav.User_id = "";
            nav.Device_id = StaticMethods.getDeviceidentifier();
            if (nav.Device_id == "unknown")
            {
                nav.Device_id = "123456";
            }
            nav.Company_name = EnumMaster.C21_MALHAR;
            nav.Tag_type = EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING;
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await Task.Delay(200);
            await Navigation.PushAsync(new PayablePage(nav));
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private async void Payable_Tapped(object sender, EventArgs e)
        {
            nav = new NavigationMdl();
            nav.Page_Title = lblPay.Text;
            nav.User_id = "";
            nav.Device_id = StaticMethods.getDeviceidentifier();
            if (nav.Device_id == "unknown")
            {
                nav.Device_id = "123456";
            }
            nav.Company_name = EnumMaster.C21_MALHAR;
            nav.Tag_type = EnumMaster.TAGTYPEPAYABLE_OUTSTANDING;
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await Navigation.PushAsync(new PayablePage(nav));
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private void CashFlow_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
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
                var loadingPage = new LoaderPage();
                await PopupNavigation.PushAsync(loadingPage);
                await Task.Delay(1000);
                await Navigation.PushAsync(new MainPage(_notificationModel));
                await Navigation.RemovePopupPageAsync(loadingPage);

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
            // DisplayAlert("Message", "Comming Soon, Approval", "ok");
            await PopupNavigation.PushAsync(new LeftMenu());
                }

        private void PrepareView()
        {
            try
            {

                if (App.ScreenWidth > 0 && App.ScreenHeight> 0)
                {
                    var calcScreenWidth = App.ScreenWidth;
                    var calcScreenHieght = App.ScreenHeight;
                    GridRec.HeightRequest =
                    GridPay.HeightRequest =
                    GridCas.HeightRequest =
                    GridCon.HeightRequest =
                    GridExp.HeightRequest =
                    GridInv.HeightRequest = calcScreenHieght / 4+20;
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
                DisplayAlert("Erro",ex.Message,"ok");
            }
        }
    }
}
