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
	public partial class Account : ContentPage
	{
		public Account ()
		{
			InitializeComponent ();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;

                FramFrom.WidthRequest =
                FramTo.WidthRequest =
                btnShow.WidthRequest = calcScreenWidth / 3;

                //TotalStock.WidthRequest = TotalGRN.WidthRequest = TotalPur.WidthRequest = calcScreenWidth / 2;
            }
        }
	}
}