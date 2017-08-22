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

            //if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            //{
            //    var calcScreenWidth = Application.Current.MainPage.Width;
            //    var calcScreenHieght = Application.Current.MainPage.Height;

            //    GridRec.HeightRequest =
            //    GridPay.HeightRequest =
            //    GridCas.HeightRequest =
            //    GridCon.HeightRequest =
            //    GridExp.HeightRequest =
            //    GridInv.HeightRequest = calcScreenHieght / 4;
            //    GridRec.WidthRequest =
            //    GridPay.WidthRequest =
            //    GridCas.WidthRequest =
            //    GridCon.WidthRequest =
            //    GridExp.WidthRequest =
            //    GridInv.WidthRequest = calcScreenWidth / 3;
            //}
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
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
            nav.Device_id = "32132";
            nav.Company_name = EnumMaster.C21_MALHAR;
            nav.Tag_type = EnumMaster.RECEIVABLE_OUTSTANDING;
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
            nav.Device_id = "32132";
            nav.Company_name = EnumMaster.C21_MALHAR;
            nav.Tag_type = EnumMaster.PAYABLE_OUTSTANDING;
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await Task.Delay(200);
            await Navigation.PushAsync(new PayablePage(nav));
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private void CashFlow_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
        }

        private void Elect_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Ele_CunsPage());
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

            //_login.Username = "sumit";// txtFName.Text;
            //_login.Password = "1";// txtPass.Text;
            //_login.DeviceID = "12345";// StaticMethods.getDeviceidentifier();
            //_login.Firebasetoken = "cq9_AqsQW1w:APA91bHRiZM2TKb3qUuf3T1NCEviyg6vPJ6K-wu7DBdkHhn8AF2oPmKsTnqtT8TdVGknGQRNDFyiqNXf_MvHXgw4gkcHDGfxSR5mUdCRuz_rtqQpqF5PoteZFvu8p8tObdckvFgZvTbi";
            //_login.Tagtype = "signin";  
            //api.PostNotification();
            try
            {
                //_loding.IsRunning = true;

                //StaticMethods.ShowToast("Please Wait");
                //await Task.Run(async() =>
                //{
                // await 
                
                var loadingPage = new LoaderPage();
                await PopupNavigation.PushAsync(loadingPage);
                await Task.Delay(1000);
                 await  Navigation.PushAsync(new MainPage());
                await Navigation.RemovePopupPageAsync(loadingPage);
                //});
            }
            catch (Exception ex)
            {
            }
            finally
            {
             
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
          
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
