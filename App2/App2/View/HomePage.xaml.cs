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
                GridInv.HeightRequest = calcScreenHieght / 5;
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
            Navigation.PushModalAsync(new ReceivablePage());
        }

        private void Payable_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PayablePage());
        }

        private void CashFlow_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
        }

        private void Elect_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Ele_CunsPage());
        }

        private void Expired_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
        }

        private void Canceled_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
        }
    }
}