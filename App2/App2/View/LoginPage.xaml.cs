using App2.APIService;
using App2.Helper;
using App2.Interface;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
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
    public partial class LoginPage : ContentPage
    {
        API api = new API();
        LoginMdl _login = new LoginMdl();

        public LoginPage(NavigationMdl mdl)
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            if (mdl.Tag_type == "paid")
            {
                Navigation.PushModalAsync(new PayableChart());
            }
            else
            {
                Navigation.PushModalAsync(new MasterMainPage());
            }
        }

        public LoginPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        private void txtPassRecognizer_Tapped(object sender, EventArgs e)
        {
            if (txtPass.IsPassword == true)
            {
                txtPass.IsPassword = false;
                signupPassshow.Source = "icon_eye_on";
            }
            else
            {
                txtPass.IsPassword = true;
                signupPassshow.Source = "icon_eye_off";
            }

        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
           
            btnLogin.IsEnabled = false;
            ResponseModel rs = new ResponseModel();
            _Loading.Color = Color.FromHex("#4472C4");
            _Loading.IsRunning = true;
            _login.Username = txtFName.Text;
            _login.Password = txtPass.Text;
            _login.DeviceID = StaticMethods.getDeviceidentifier(); 
            if (_login.DeviceID == "unknown")
            {
                _login.DeviceID = "123456";
            }
            if (Device.OS == TargetPlatform.iOS)
            {
                _login.IosToken = DependencyService.Get<IIosMethods>().GetTokan();
            }
            else
            {
                _login.Firebasetoken = DependencyService.Get<IAndroidMethods>().GetTokan();
                if (_login.Firebasetoken == null)
                {
                    _login.Firebasetoken = "";
                }
            }
            _login.Tagtype = EnumMaster.SIGNIN;

            try
            {
                if (txtFName.Text != string.Empty && txtPass.Text != string.Empty)
                {
                    rs = await api.PostLogin(_login);
                    if (rs.Error == "False")
                    {
                        rs.Device_Id = _login.DeviceID;
                        StaticMethods.SaveLocalData(rs);
                        await Navigation.PushPopupAsync(new LoginSuccessPopupPage("S", "Successfully Login"));
                        await Navigation.PushModalAsync(new MasterMainPage());
                        txtFName.Text = txtPass.Text = string.Empty;
                    }
                }
                else if (txtFName.Text == string.Empty && txtPass.Text == string.Empty)
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "Please Fill All Details"));
                }
                else if (txtFName.Text == string.Empty)
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "Please Fill User Name"));
                }
                else if (txtPass.Text == string.Empty)
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "Please Fill Password"));
                }
                //if (IsLoginSuccess == true)
                //{
                //    await Navigation.PushPopupAsync(new LoginSuccessPopupPage());
                //    await Navigation.PushModalAsync(new MasterMenuPage());
                //    txtFName.Text = txtPass.Text = string.Empty;
                //}
                
                _Loading.IsRunning = false;
                btnLogin.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", ex.Message));
            }
            
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}