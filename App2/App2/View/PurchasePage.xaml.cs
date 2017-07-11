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
    public partial class PurchasePage : ContentPage
    {
        public PurchasePage()
        {
            InitializeComponent();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;

                lblPurchase.WidthRequest =
                lblIndent.WidthRequest =
                lblEnquary.WidthRequest =
                lblQuatation.WidthRequest =
                lblPurOrder.WidthRequest =
                lblGNR.WidthRequest = calcScreenWidth / 2 ;
                    
                TotalStock.WidthRequest =TotalGRN.WidthRequest =TotalPur.WidthRequest =calcScreenWidth / 2;
            }
        }
    }
}