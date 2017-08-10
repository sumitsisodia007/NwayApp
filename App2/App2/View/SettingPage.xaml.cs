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
	public partial class SettingPage : ContentPage
	{
		public SettingPage ()
		{
			InitializeComponent ();
            var calcScreenWidth = Application.Current.MainPage.Width;
            var calcScreenHieght = Application.Current.MainPage.Height;
            CircleImg.WidthRequest = calcScreenWidth / 4;
            CircleImg.HeightRequest = calcScreenWidth / 4;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotificationSetting());
        }
    }
}