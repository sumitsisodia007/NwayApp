using App2.APIService;
using App2.NativeMathods;
using App2.View;
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
    public partial class LogOutPage : PopupPage
    {
        //API api = null;
        //App2.Model.UserModel res=null;
        //App2.Model.NavigationMdl nav = null;
        public LogOutPage()
        {
            InitializeComponent();
        }
        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }
        
        private void Cancel_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
        private void Logout_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
            //api = new API();
            //nav = new Model.NavigationMdl();
            //res = StaticMethods.GetLocalSavedData();
            StaticMethods.DeleteLocalData();
            var myAppsFirstPage = new LoginPage();
            Application.Current.MainPage = new NavigationPage(myAppsFirstPage);
        }
        protected override bool OnBackgroundClicked()
        {
            return base.OnBackgroundClicked();
        }
    }
}