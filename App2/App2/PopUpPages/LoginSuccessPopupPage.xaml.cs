﻿using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginSuccessPopupPage : PopupPage
    {
        //FF2E27Error ,FF9D00 warning, Success 006500, Info 0077FF
        public LoginSuccessPopupPage()
        {
            InitializeComponent();
        }
        public LoginSuccessPopupPage(string mtitle, string msg)
        {
            InitializeComponent();
            ChangecolorMsg(mtitle, msg);
        }
        private async void ChangecolorMsg(string mtitle, string msg)
        {
            if (mtitle=="W")
            {
                StkMessage.BackgroundColor = Color.FromHex("#0077FF");
            }else if (mtitle == "S")
            {
                ImgAlert.Source = "check";
                StkMessage.BackgroundColor = Color.FromHex("#43A047");
            }else if (mtitle == "E")
            {
                ImgAlert.Source = "cancel";
                StkMessage.BackgroundColor = Color.FromHex("#FF2E27");
            }
            LblMessage.Text = msg;
            await Task.Delay(500);
            await Task.WhenAll(
             ImgAlert.ScaleTo(2, 1000),
             LblMessage.ScaleTo(1.5, 1000),
             ImgAlert.RotateTo(360, 1000)
             );
            //await imgAlert.TranslateTo(-100, -100, 1000);
            //await imgAlert.FadeTo(1, 2000);
            //await imgAlert.TranslateTo(-100, 0, 1000);    // Move image left
            //await imgAlert.TranslateTo(-100, -100, 1000); // Move image up
            //await imgAlert.TranslateTo(100, 100, 2000);   // Move image diagonally down and right
            //await imgAlert.TranslateTo(0, 100, 1000);     // Move image left
            //await imgAlert.TranslateTo(0, 0, 1000);       // Move image up
           // await imgAlert.TranslateTo(0, 200, 2000, Easing.BounceIn);
            //await imgAlert.ScaleTo(2, 2000, Easing.CubicIn);
            //await imgAlert.RotateTo(360, 2000, Easing.SinInOut);
            //await imgAlert.ScaleTo(1, 2000, Easing.CubicOut);
           // await imgAlert.TranslateTo(0, -200, 2000, Easing.BounceOut);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(4000);
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}