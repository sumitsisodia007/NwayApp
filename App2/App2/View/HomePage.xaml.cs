using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
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
    public partial class HomePage : ContentPage
    {
        NavigationMdl nav = null;
        LoginMdl _login = new LoginMdl();
        API api=new API();
        
        public HomePage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }


        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                lblNotificationBadge.Text = StaticMethods.NotificationCount.ToString();
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
                {
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;
                    GridRec.HeightRequest =
                    GridPay.HeightRequest =
                    GridCas.HeightRequest =
                    GridCon.HeightRequest =
                    GridExp.HeightRequest =
                    GridInv.HeightRequest = calcScreenHieght / 4;
                    GridRec.WidthRequest =
                    GridPay.WidthRequest =
                    GridCas.WidthRequest =
                    GridCon.WidthRequest =
                    GridExp.WidthRequest =
                    GridInv.WidthRequest = calcScreenWidth / 3;
                }
            });
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
            {  var loadingPage = new LoaderPage();
                await PopupNavigation.PushAsync(loadingPage);
                await Task.Delay(1000);
                 await  Navigation.PushAsync(new MainPage());
                await Navigation.RemovePopupPageAsync(loadingPage);
               
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnDisappearing()
        {
            
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
    }
}
