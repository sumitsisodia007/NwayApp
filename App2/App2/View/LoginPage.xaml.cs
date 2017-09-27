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
        private API api = new API();
        private LoginMdl _login = new LoginMdl();

        public LoginPage(NavigationMdl mdl)
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            if (mdl.TagType == "paid")
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
           
          //  btnLogin.IsEnabled = false;
            ResponseModel rs = new ResponseModel();
            LoginResponseMdl res = new LoginResponseMdl();
            _Loading.Color = Color.FromHex("#4472C4");
            _Loading.IsRunning = true;
           rs.UserName= _login.Username = txtFName.Text;
           rs.Password= _login.Password = txtPass.Text;
            _login.Tagtype = EnumMaster.TagtypeSignin;
            _login.DeviceId = StaticMethods.GetDeviceidentifier(); 
            if (_login.DeviceId == "unknown")
            {
                _login.DeviceId = "123456";
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
           

            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
                }
                else
                {
                    if (txtFName.Text != string.Empty && txtPass.Text != string.Empty)
                    {
                        res =  api.PostLogin(_login);
                       // StaticMethods._login_response = res;
                        if (res.Error == "false")
                        {
                            rs.DeviceId = _login.DeviceId;
                            rs.MinReceiptAmt = res.MinReceiptAmount.ToString();
                            rs.NotificationDayCount = res.NotificationDayCount.ToString();
                            rs.Error = res.Error;
                            rs.UserId = res.UserId.ToString();
                            StaticMethods.SaveLocalData(rs);
                            await Navigation.PushPopupAsync(new LoginSuccessPopupPage("S", res.Message));
                            await Navigation.PushModalAsync(new MasterMainPage(res));
                            txtFName.Text = txtPass.Text = string.Empty;
                        }
                        else if (res.Error == "true")
                        {
                            await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", res.Message));
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
                }
                _Loading.IsRunning = false;
              //  btnLogin.IsEnabled = true;
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