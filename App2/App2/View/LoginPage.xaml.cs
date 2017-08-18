using App2.APIService;
using App2.Helper;
using App2.Interface;
using App2.Model;
using App2.NativeMathods;
using Plugin.Connectivity;
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
                Navigation.PushModalAsync(new MasterMenuPage());
            }
        }

        public LoginPage(string Status)
        {
            InitializeComponent();
            Navigation.PushModalAsync(new MasterMenuPage());
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
            _login.DeviceID = "123456 ";// StaticMethods.getDeviceidentifier();
            _login.Firebasetoken = StaticMethods.getTokan();//"asdgasdggshgdj";
            _login.Tagtype = EnumMaster.SIGNIN;
            await Task.Run(() =>
            {
                if (txtFName.Text != string.Empty && txtPass.Text != string.Empty)
                {
                    ResponseModel res = StaticMethods.GetLocalSavedData();
                    rs = api.postLogin(_login);
                    if (rs.Error == "False")
                    {
                        rs.Device_Id = _login.DeviceID;
                        rs.Min_Receipt_Amt = res.Min_Receipt_Amt;
                        StaticMethods.SaveLocalData(rs);
                  
                        //StaticMethods.ShowToast(rs.Message);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Navigation.PushModalAsync(new MasterMenuPage());
                        });
                    }
                    else
                    {

                    }

                }
                else if (txtFName.Text == string.Empty && txtPass.Text == string.Empty)
                { StaticMethods.ShowToast("Please Fill All Details"); }
                else if (txtFName.Text == string.Empty)
                { StaticMethods.ShowToast("Please Fill User Name"); }
                else if (txtPass.Text == string.Empty)
                { StaticMethods.ShowToast("Please Fill Password"); }
            });

            txtFName.Text = txtPass.Text = string.Empty;
            _Loading.IsRunning = false;
            btnLogin.IsEnabled = true;
        }

        protected async override void OnAppearing()
        {
            ResponseModel res = StaticMethods.GetLocalSavedData();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}