using Rg.Plugins.Popup.Pages;
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
            changecolorMsg(mtitle, msg);
        }
        private void changecolorMsg(string mtitle, string msg)
        {
            if (mtitle=="W")
            {
                stkMessage.BackgroundColor = Color.FromHex("#0077FF");
            }else if (mtitle == "S")
            {
                stkMessage.BackgroundColor = Color.FromHex("#43A047");
            }else if (mtitle == "E")
            {
                stkMessage.BackgroundColor = Color.FromHex("#FF2E27");
            }
            lblMessage.Text = msg;
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