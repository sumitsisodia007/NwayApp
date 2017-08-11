using App2.APIService;
using App2.Interface;
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

        public LoginPage(NavigationMdl mdl)
        {
            InitializeComponent();
            if (mdl.Tag_type == "paid")
            {
                Navigation.PushModalAsync(new PayableChart());
            }
            else
            {
                Navigation.PushModalAsync(new MasterMenuPage());
            }
        }

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

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            ResponseModel rs=new ResponseModel();
            _Loading.Color = Color.FromHex("#4472C4");
            _Loading.IsRunning = true;
            _login.Username = txtFName.Text;
            _login.Password =  txtPass.Text;
            _login.DeviceID = "123456";// StaticMethods.getDeviceidentifier();
            _login.Firebasetoken =  StaticMethods.getTokan();//"asdgasdggshgdj";
            _login.Tagtype = "signin";
            await Task.Run( () =>
            {
                if (txtFName.Text != string.Empty && txtPass.Text != string.Empty)
                {
                    rs = api.postLogin(_login);
                    rs.Device_Id = _login.DeviceID;
                    StaticMethods.SaveLocalData(rs);
                }
                else if (txtFName.Text == string.Empty && txtPass.Text == string.Empty)
                { StaticMethods.ShowToast("Please Fill All Details"); }
                else if (txtFName.Text == string.Empty )
                { StaticMethods.ShowToast("Please Fill User Name"); }
                else if(txtPass.Text == string.Empty)
                { StaticMethods.ShowToast("Please Fill Password"); }
            });
            if (rs.Error == "False")
            {
                StaticMethods.ShowToast(rs.Message);
                await Navigation.PushModalAsync(new MasterMenuPage());
            }
           
            _Loading.IsRunning = false;
        }
    }
}