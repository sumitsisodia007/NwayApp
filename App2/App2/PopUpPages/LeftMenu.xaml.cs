using App2.Model;
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
         public List<SiteNameMdl> menuList { get; set; }
        public List<string> tmplist { get; set; }
        
        public LeftMenu()
        {
            InitializeComponent();
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
            menuList = new List<SiteNameMdl>();
            menuList.Add(new SiteNameMdl { DefaultText = "Site A", CheckedText = "Checked Site A", UncheckedText = "Unchecked Site A" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site B", CheckedText = "Checked Site B", UncheckedText = "Unchecked Site B" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site C", CheckedText = "Checked Site C", UncheckedText = "Unchecked Site C" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site D", CheckedText = "Checked Site D", UncheckedText = "Unchecked Site D" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site E", CheckedText = "Checked Site E", UncheckedText = "Unchecked Site E" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site F", CheckedText = "Checked Site F", UncheckedText = "Unchecked Site F" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site G", CheckedText = "Checked Site G", UncheckedText = "Unchecked Site G" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site H", CheckedText = "Checked Site H", UncheckedText = "Unchecked Site H" });
            menuList.Add(new SiteNameMdl { DefaultText = "Site I", CheckedText = "Checked Site I", UncheckedText = "Unchecked Site I" });
            NavigationList.ItemsSource = menuList;
        }

        private async void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CheckBox isCheckedOrNot = (CheckBox)sender;
            var selectedStudent = isCheckedOrNot.BindingContext as SiteNameMdl;
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