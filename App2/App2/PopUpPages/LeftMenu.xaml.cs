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
using AsNum.XFControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App2.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeftMenu : PopupPage
    {
        //   public List<SiteIdMdl> menuList { get; set; }
        public List<CompanySite> menuList { get; set; }
        private List<TempSiteIdMdl> tempchlst = null;
        

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
                _companyname.Add(new ShowCompanyNameMdl { CompanyName = item.CompanyName });
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
                    var res = StaticMethods.GetLocalSavedData();
                    res.CompanyName = lblupdate.Text = StaticMethods.SetCompanyName = (string)picker.Items[selectedIndex];
                    res.CompanyIndex = selectedIndex.ToString();
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
                if (res.CompanyIndex != null)
                {
                    MainPickr.SelectedIndex = Convert.ToInt32(res.CompanyIndex);
                }
            }
        }

        public void DrawalMenu()
        {
            tempchlst = new List<TempSiteIdMdl>();
            menuList = new List<CompanySite>();
            foreach (var item in _data._permissions)
            {
                if (StaticMethods.SetCompanyName == item.CompanyName)
                {
                    foreach (var item2 in item.Sites)
                    {
                        tempchlst.Add(new TempSiteIdMdl{ SiteId = item2.Site_id, SiteName = item2.Site_name, SiteShortName = item2.Site_short_name });
                        menuList.Add(new CompanySite { Site_id = item2.Site_id, Site_name = item2.Site_name ,Site_short_name=item2.Site_short_name,Chk_id=item2.Chk_id});
                    }
                }
            }
            NavigationList.ItemsSource = menuList;
            lblupdate.Text = StaticMethods.SetCompanyName;
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
            var mychkArray= tempchlst.Select(x => x.SiteName).ToArray();

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

        private void CheckBox_CheckedChanged(object sender, EventArgs eventArgs)
        {
            try
            {
                CheckBox isCheckedOrNot = (CheckBox)sender ;
                var name = (CompanySite)isCheckedOrNot.BindingContext;
                //if (isCheckedOrNot.Checked == true)
                //{
                //    tempchlst.Add(new TempSiteIdMdl { SiteId = name.Site_id, SiteName = name.Site_name, SiteShortName = name.Site_short_name});
                //}
                //else
                //{
                //    var itemToRemove = tempchlst.Single(r => r.SiteId == name.Site_id);
                //    tempchlst.Remove(itemToRemove);
                //}

                foreach (var item in _data._permissions)
                {
                    foreach (var item2 in item.Sites)
                    {
                        if (item2.Site_name != name.Site_name) continue;
                        if (isCheckedOrNot.Checked == true)
                        {
                            item2.Chk_id = true;
                            tempchlst.Add(new TempSiteIdMdl { SiteId = name.Site_id, SiteName = name.Site_name, SiteShortName = name.Site_short_name });
                        }
                        else
                        {
                            item2.Chk_id = false;
                            var itemToRemove = tempchlst.Single(r => r.SiteId == name.Site_id);
                            tempchlst.Remove(itemToRemove);
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