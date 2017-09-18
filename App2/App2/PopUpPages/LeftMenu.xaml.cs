using App2.Model;
using App2.NativeMathods;
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
         public List<Site_id_Mdl> menuList { get; set; }
        public List<string> tmplist { get; set; }
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
            PrepareLayout();
            DrawalMenu();
        }

        private void PrepareLayout()
        {
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                stkMessage.HeightRequest = calcScreenHieght;
                stkMessage.WidthRequest = calcScreenWidth - 100;
            }
        }

        public void DrawalMenu()
        {
            menuList = new List<Site_id_Mdl>();
            foreach (var item in _data._permissions)
            {
                if (StaticMethods.Set_Company_Name == item.Company_name)
                {
                    foreach (var item2 in item._company_site)
                    {

                        menuList.Add(new Site_id_Mdl{ Site_id = item2.Site_id, SiteName = item2.Site_name });
                    }
                }
            }
            NavigationList.ItemsSource = menuList;
        }

        private async void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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
            await PopupNavigation.RemovePageAsync(this);
        }

        protected override bool OnBackgroundClicked()
        {
            return base.OnBackgroundClicked();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void CheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            CheckBox isCheckedOrNot = (CheckBox)sender;
            var name = isCheckedOrNot.DefaultText;
            tmplist = new List<string>();
            tmplist.Add(name);
        }
    }
}