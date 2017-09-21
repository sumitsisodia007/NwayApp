using App2.Model;
using App2.NativeMathods;
using App2.ShowModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace App2.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeftMenu : PopupPage
    {
        //   public List<Site_id_Mdl> menuList { get; set; }
        public List<Company_site> menuList { get; set; }
        private List<Temp_Site_id_Mdl> tempchlst = null;
        

        public List<ShowCompanyNameMdl> _companyname { get; set; }
        LoginResponseMdl _data;

        public LeftMenu()
        {
            InitializeComponent();
            PrepareLayout();
            DrawalMenu();
        }
        public LeftMenu(LoginResponseMdl data)
        {
            InitializeComponent();
            _data = data;
            PickerData(data);
            DrawalMenu();
            PrepareLayout();
        }
        private void PickerData(LoginResponseMdl res)
        {
            _companyname = new List<ShowCompanyNameMdl>();
            foreach (var item in res._permissions)
            {
                _companyname.Add(new ShowCompanyNameMdl { CompanyName = item.Company_name });
            }
            MainPickr.ItemsSource = _companyname;
        }

        private void MainPickr_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                try
                {

                    ResponseModel res = new ResponseModel();
                    res = StaticMethods.GetLocalSavedData();
                    res.Company_Name = lblupdate.Text = StaticMethods.Set_Company_Name = (string)picker.Items[selectedIndex];
                    res.Company_Index = selectedIndex.ToString();
                    StaticMethods.SaveLocalData(res);
                    DrawalMenu();
                }
                catch (Exception ex)
                {
                }

            }
        }

        private void PrepareLayout()
        {
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                stkMessage.HeightRequest = calcScreenHieght;
                stkMessage.WidthRequest = calcScreenWidth - 100;
                ResponseModel res = StaticMethods.GetLocalSavedData();
                if (res.Company_Index != null)
                {
                    MainPickr.SelectedIndex = Convert.ToInt32(res.Company_Index);
                }
            }
        }

        public void DrawalMenu()
        {
            tempchlst = new List<Temp_Site_id_Mdl>();
            menuList = new List<Company_site>();
            foreach (var item in _data._permissions)
            {
                if (StaticMethods.Set_Company_Name == item.Company_name)
                {
                    foreach (var item2 in item._company_site)
                    {
                        tempchlst.Add(new Temp_Site_id_Mdl{ Site_id = item2.Site_id, SiteName = item2.Site_name, Site_short_name = item2.Site_short_name });
                        menuList.Add(new Company_site { Site_id = item2.Site_id, Site_name = item2.Site_name ,Site_short_name=item2.Site_short_name,Chk_id=item2.Chk_id});
                    }
                }
            }
            NavigationList.ItemsSource = menuList;
            lblupdate.Text = StaticMethods.Set_Company_Name;
        }

        private void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CheckBox isCheckedOrNot = (CheckBox)sender;
           // var selectedStudent = isCheckedOrNot.BindingContext as SiteNameMdl;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //  HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(2000);
            await PopupNavigation.RemovePageAsync(this);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var data = _data;
            

            await Task.WhenAll(
             Navigation.PushModalAsync(new App2.View.MasterMainPage(_data, tempchlst)),
             PopupNavigation.RemovePageAsync(this)
            );
        }

        protected override bool OnBackgroundClicked()
        {
            return base.OnBackgroundClicked();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MainPickr.Focus();
            });
        }

        private void CheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                CheckBox isCheckedOrNot = (CheckBox)sender ;
                var name = (Company_site)isCheckedOrNot.BindingContext;
                if (isCheckedOrNot.Checked == true)
                {
                    
                    tempchlst.Add(new Temp_Site_id_Mdl { Site_id = name.Site_id, SiteName = name.Site_name, Site_short_name = name.Site_short_name});
                }
                else
                {
                    var itemToRemove = tempchlst.Single(r => r.Site_id == name.Site_id);
                    tempchlst.Remove(itemToRemove);
                }

                foreach (var item in _data._permissions)
                {
                    foreach (var item2 in item._company_site)
                    {
                        if (item2.Site_name == name.Site_name)
                        {
                            if (isCheckedOrNot.Checked == true)
                            {
                                item2.Chk_id = true;
                            }
                            else
                            {
                                item2.Chk_id = false;
                            }
                        }
                    }
                }
                StaticMethods.userCh = tempchlst;
            }
            catch (Exception ex)
            {
            }
        }
    }
}