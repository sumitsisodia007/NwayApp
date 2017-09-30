using App2.APIService;
using App2.Helper;
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
    public partial class MasterMainPage : MasterDetailPage
    {
        private LoginResponseMdl _newres;
        private NavigationMdl _objNav = null;
        private CompanyTbl cmpTbl = null;
        readonly API _api = new API();
        public MasterMainPage(LoginResponseMdl res, List<TempSiteIdMdl> tempchlst)
        {
            InitializeComponent();
            _newres = res;
            this.Master = new MasterPage(_newres);
            this.Detail = new NavigationPage(new HomePage(_newres, tempchlst));
            App.MasterDetail = this;
        }

        public MasterMainPage(LoginResponseMdl res)
        {
            InitializeComponent();
            //_newres = res;
            StaticMethods._new_res = _newres = res;
            SetBedge();
            this.Master = new MasterPage(_newres);
            this.Detail = new NavigationPage(new HomePage(_newres));
            App.MasterDetail = this;
        }

        public MasterMainPage(NavigationMdl mdl)
        {
            MainLogin();
            InitializeComponent();

            this.Master = new MasterPage(_newres);
            this.Detail = new NavigationPage(new HomePage(_newres, mdl));
            App.MasterDetail = this;
        }

        public MasterMainPage()
        {
            MainLogin();
            InitializeComponent();

            this.Master = new MasterPage(_newres);
            this.Detail = new NavigationPage(new HomePage(_newres));
            App.MasterDetail = this;
        }




        private  void MainLogin()
        {
           
            LoginResponseMdl res = new LoginResponseMdl();
            LoginMdl _login = new LoginMdl();
           
            UserModel rs = StaticMethods.GetLocalSavedData();
            _login.Username = rs.UserName;
            _login.Password = rs.Password;
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
            res = _api.PostLogin(_login);
            if (res.Error == "false")
            {
                //_newres = res;
                StaticMethods._new_res = _newres = res;
            }
            SetBedge();
        }

        
        private void SetBedge()
        {
            try
            {

            UserModel rs = StaticMethods.GetLocalSavedData();
            _objNav = new NavigationMdl();
            NavigationMdl nav = _objNav.PrepareApiData();

            NotificationListMdl notificationModel = _api.PostNotification(nav);
            var d2 = DateTime.Now.ToString("dd-MMM-yyyy");
            string dateChk = null;
            string notcount = "0";
            
            foreach (var item in notificationModel.ListNotificationDate)
            {
                dateChk = item.Date;
                notcount = item.NotCount;
                break;
            }
            rs.NotCount = d2.ToString() == dateChk ? notcount : "0";
            StaticMethods.SaveLocalData(rs);

            }
            catch (Exception e)
            {
            }
        }
    }
}