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
    public partial class ReceivablePage : ContentPage
    {
        public List<ReceivableMdl> _receivable { get; set; }
        API api = new API();
        public static int flag = 0;
        public ReceivablePage()
        {
            InitializeComponent();
            flag = 1;
            _receivable = api.ReceivableTable();

            listView.ItemsSource = _receivable;
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                LblH1.WidthRequest =
                LblH2.WidthRequest =
                LblH3.WidthRequest =
                LblH4.WidthRequest = calcScreenWidth / 4 - 20;
            }      
        }         
                    

        private void Row_Tapped(object sender, EventArgs e)
        {
           // Navigation.PushModalAsync(new ReceivableChart());
        }
    }


    public class PartyDetails
    {
        public string Party { get; set; }
        public string Pre_Outstanding { get; set; }
        public string Today_Receipt { get; set; }
        public string Cur_Outstanding { get; set; }
        public double txtWidth { get; set; }
    }
}