using App2.APIService;
using App2.Model;
using App2.NativeMathods;
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
        public LoginPage()
        {
            InitializeComponent();
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

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            _login.Username = "elensoft";// txtFName.Text;
            _login.Password = "123";// txtPass.Text;
            _login.DeviceID = "12345";// StaticMethods.getDeviceidentifier();
            _login.Firebasetoken = "asdgasdggshgdj";
            _login.Tagtype = "signin";
            //api.postLogin(_login);
            Navigation.PushModalAsync(new MasterMenuPage());
        }
    }
}