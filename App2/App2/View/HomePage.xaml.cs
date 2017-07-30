using App2.APIService;
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
    public partial class HomePage : ContentPage
    {
        LoginMdl _login = new LoginMdl();
        API api=new API();
        public HomePage()
        {
            InitializeComponent();
          
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
        }

        private  void Receivable_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReceivablePage());
        }

        private  void Payable_Tapped(object sender, EventArgs e)
        {
           // NavigationPage.SetHasNavigationBar(new PayablePage(), false);

             Navigation.PushAsync(new PayablePage());
            
           // NavigationPage.SetHasNavigationBar(new PayablePage(), true);
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

        private void Notification_Clicked(object sender, EventArgs e)
        {

            _login.Username = "";// txtFName.Text;
            _login.Password = "";// txtPass.Text;
            _login.DeviceID = "12345";// StaticMethods.getDeviceidentifier();
            _login.Firebasetoken = "asdgasdggshgdj";
            _login.Tagtype = "signin";
            //api.PostNotification();
            Navigation.PushAsync(new MainPage());
        }
    }
}