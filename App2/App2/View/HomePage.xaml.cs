using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using App2.ShowModels;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        NavigationMdl obj_nav = null;
        LoginMdl _login = new LoginMdl();
        API api = new API();
        LoginResponseMdl _newres;
        NotificationListMdl _notificationModel;
        List<Temp_Site_id_Mdl> _new_tempchlst;

        public HomePage()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            //PrepareView();

            
                Task.Delay(1000);
                SetNotificationBadge();
            });
        }
        public HomePage(LoginResponseMdl res)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            StaticMethods._new_res = _newres = res;
            PrepareView(res);
            Task.Delay(500);
             //   SetNotificationBadge();
            });
        }

        public HomePage(LoginResponseMdl res, NavigationMdl mdl)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                StaticMethods._new_res = _newres = res;
                PrepareView(res);
                Task.Delay(500);

                NavigatePageNotification(res,mdl);
            });
        }
        private async void NavigatePageNotification(LoginResponseMdl lgres, NavigationMdl nav)
        {
            ObservableCollection<Site_id_Mdl> lst = new ObservableCollection<Site_id_Mdl>();
            StaticMethods._new_res = _newres = lgres;
            ResponseModel res = StaticMethods.GetLocalSavedData();
            foreach (var item in lgres._permissions)
            {
                if (StaticMethods.Set_Company_Name == item.Company_name)
                {
                    foreach (var item2 in item._company_site)
                    {

                        lst.Add(new Site_id_Mdl { Site_id = item2.Site_id, SiteName = item2.Site_name });
                    }
                    nav.Company_Id = item.Company_id.ToString();
                }
            }
            nav.User_name = res.UserName;
            nav.Password = res.Password;
            nav.Device_id = res.Device_Id;
            nav.User_id = res.User_Id;
            nav._site_Id = lst;
            nav.Party_id = "1";
            if (nav.Tag_type == "payable_outstanding")
            {
                nav.Page_Title = lblPay.Text;
                
            }
            else if (nav.Tag_type == "receivable_outstanding")
            {
                nav.Page_Title = lblReceive.Text;
            }
            //nav.Tag_type = nav.Tag_type;
            await Navigation.PushAsync(new PayablePage(nav));
        }

        public HomePage(LoginResponseMdl res, List<Temp_Site_id_Mdl> tempchlst)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                StaticMethods._new_res = _newres = res;
                _new_tempchlst = tempchlst;
                PrepareView(res);
                Task.Delay(500);
                //   SetNotificationBadge();
            });
        }

      


        private async void SetNotificationBadge()
        {
            try
            {
                string DateChk = null;
                string Notcount = "0";
                ResponseModel rs = StaticMethods.GetLocalSavedData();
                api = new API();
                NavigationMdl nav = new NavigationMdl();
                nav.Device_id = StaticMethods.getDeviceidentifier();
                if (nav.Device_id == "unknown")
                {
                    nav.Device_id = "123456";
                }
                nav.Company_name = EnumMaster.C21_MALHAR;
                nav.Tag_type = EnumMaster.TAGTYPENOTIFICATIONS;

                nav.User_id = rs.User_Id;
               // _notificationModel = api.PostNotification(nav);


                var d2 = DateTime.Now.ToString("dd-MMM-yyyy");
                foreach (var item in _notificationModel.ListNotificationDate)
                {
                    DateChk = item.Date;
                    Notcount = item.NotCount;
                    break;
                }
                if (d2.ToString() == DateChk)
                {
                    rs.NotCount = lblNotificationBadge.Text = Notcount;
                }
                else
                {
                    lblNotificationBadge.Text = "0";
                    //rs.NotCountDate = DateChk;
                    //StaticMethods.SaveLocalData(rs);
                }
            }
            catch (Exception ex)
            {
                //  await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
        }
        
        private async void Receivable_Tapped(object sender, EventArgs e)
        {
            //ResponseModel res = new ResponseModel();
            //nav = new NavigationMdl();
            //nav.Page_Title = lblReceive.Text;

            //nav.User_id = res.User_Id;
            //nav.Device_id = res.Device_Id;
            //if (nav.Device_id == "unknown")
            //{
            //    nav.Device_id = "123456";
            //}
            //nav.Company_name = res.Company_Name;
            try
            {
                obj_nav = new NavigationMdl();
                NavigationMdl nav = obj_nav.PrepareAPIData();
                // PayableNotificationMdl _payable = await api.PayableTable(nav);
                nav.Page_Title = lblReceive.Text;
                nav.Tag_type = EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING;
                await Task.Delay(200);
                await Navigation.PushAsync(new PayablePage(nav));

            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            
        }

        private async void Payable_Tapped(object sender, EventArgs e)
        {
            obj_nav = new NavigationMdl();
            NavigationMdl nav = obj_nav.PrepareAPIData();
            nav.Page_Title = lblPay.Text;
            
            nav.Tag_type = EnumMaster.TAGTYPEPAYABLE_OUTSTANDING;
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            await Navigation.PushAsync(new PayablePage(nav));
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        private void CashFlow_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
        }

        private async void Elect_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ele_CunsPage());
        }

        private void Expired_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExpiredSoon());
        }

        private void Canceled_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon", "ok");
        }

        private async void Notification_Clicked(object sender, EventArgs e)
        {
            try
            {
                //var loadingPage = new LoaderPage();
                //await PopupNavigation.PushAsync(loadingPage);
                //await Task.Delay(1000);

                //await Navigation.PushAsync(new MainPage(_notificationModel));
                //await Navigation.RemovePopupPageAsync(loadingPage);
                obj_nav = new NavigationMdl();
                NavigationMdl nav = obj_nav.PrepareAPIData();
                NotificationListMdl _nmdl = api.PostNotification(nav,_new_tempchlst);
                if (_nmdl.Error == "true")
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", _nmdl.Message));
                }
                else
                {
                    var loadingPage = new LoaderPage();
                    await PopupNavigation.PushAsync(loadingPage);
                    await Task.Delay(500);
                    await Navigation.PushAsync(new MainPage(_nmdl));
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void MainManuSlide_Tapped(object sender, EventArgs e)
        {
            if ((App.MasterDetail.IsPresented) == false)
            {
                App.MasterDetail.IsPresented = true;
            }
        }

        private void Alert_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Message", "Comming Soon, Alert", "ok");
        }

        private async void Approval_Clicked(object sender, EventArgs e)
        {
           await  DisplayAlert("Message", "Comming Soon, Approval", "ok");
        }

        //private NavigationMdl PrepareAPIData()
        //{
        //    NavigationMdl nav = new NavigationMdl();
        //    ObservableCollection<Site_id_Mdl> lst = new ObservableCollection<Site_id_Mdl>();
        //    ResponseModel res = StaticMethods.GetLocalSavedData();
        //    try
        //    {
        //        foreach (var item in _newres._permissions)
        //        {
        //            if (StaticMethods.Set_Company_Name == item.Company_name)
        //            {
        //                foreach (var item2 in item._company_site)
        //                {

        //                    lst.Add(new Site_id_Mdl { Site_id = item2.Site_id, SiteName = item2.Site_name });
        //                }
        //                nav.Company_Id = item.Company_id.ToString();
        //            }
        //        }
        //        nav.User_name = res.UserName;
        //        nav.Password = res.Password;
        //        nav.Device_id = res.Device_Id;
        //        nav.User_id = res.User_Id;
        //        nav.Tag_type = EnumMaster.TAGTYPENOTIFICATIONS;
        //        nav._site_Id = lst;
        //        nav.Party_id = "1";
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return nav;
        //}

        private void PrepareView(LoginResponseMdl res)
        {
            try
            {
                ResponseModel res1 = StaticMethods.GetLocalSavedData();
                if (res1.Company_Name != null)
                {
                    lblSetCom_Name.Text = StaticMethods.Set_Company_Name=res1.Company_Name;
                }
                else
                {
                    foreach (var item in res._permissions)
                    {
                        StaticMethods.Set_Company_Name= lblSetCom_Name.Text = item.Company_name;
                        break;
                    }
                }
                if (App.ScreenWidth > 0 && App.ScreenHeight > 0)
                {
                    var calcScreenWidth = App.ScreenWidth;
                    var calcScreenHieght = App.ScreenHeight;
                    GridRec.HeightRequest =
                    GridPay.HeightRequest =
                    GridCas.HeightRequest =
                    GridCon.HeightRequest =
                    GridExp.HeightRequest =
                    GridInv.HeightRequest = calcScreenHieght / 4 + 20;
                    GridRec.WidthRequest =
                    GridPay.WidthRequest =
                    GridCas.WidthRequest =
                    GridCon.WidthRequest =
                    GridExp.WidthRequest =
                    GridInv.WidthRequest = calcScreenWidth / 3;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "ok");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new LeftMenu(_newres));
        }
    }
}
