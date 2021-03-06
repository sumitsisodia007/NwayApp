﻿using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using App2.ShowModels;
using Rg.Plugins.Popup.Services;
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
    public partial class MasterPage : ContentPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public List<ShowCompanyNameMdl> Companyname { get; set; }

        public MasterPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            DrawalMenu();
           // PickerData();
        }

        public MasterPage(LoginResponseMdl res)
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            DrawalMenu();
            //PickerData(res);
            //PreLayout();
        }

        private void PickerData(LoginResponseMdl res)
        {
            try
            {
                Companyname = new List<ShowCompanyNameMdl>();
                foreach (var item in res._permissions)
                {
                    Companyname.Add(new ShowCompanyNameMdl { CompanyName = item.CompanyName });
                }
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast("Internal Error in Picker"+exception.Message);
            }
            
            //SSS 27-Sep
            //MainPickr.ItemsSource = _companyname;
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                if (!(Application.Current.MainPage.Width > 0) || !(Application.Current.MainPage.Height > 0)) return;
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                //SSS 27-Sep
                //MainPickr.WidthRequest = calcScreenWidth -80;
                //UserModel res = StaticMethods.GetLocalSavedData();
                //if (res.CompanyIndex != null)
                //{
                //    MainPickr.SelectedIndex = Convert.ToInt32(res.CompanyIndex);
                //}
            });
        }

        private void PreLayout()
        {
            Device.BeginInvokeOnMainThread(() => {
                if (!(Application.Current.MainPage.Width > 0) || !(Application.Current.MainPage.Height > 0)) return;
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                //SSS 27-Sep
                //MainPickr.WidthRequest = calcScreenWidth - 80;
                //UserModel res = StaticMethods.GetLocalSavedData();
                //if (res.CompanyIndex != null)
                //{
                //    MainPickr.SelectedIndex = Convert.ToInt32(res.CompanyIndex);
                //}
            });
        }

        public void DrawalMenu()
        {
            menuList = new List<MasterPageItem>();

            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            var page1 = new MasterPageItem() { Title = "Home", Icon = "home", TargetType = typeof(HomePage) };
            var page2 = new MasterPageItem() { Title = "Sales", Icon = "sales", TargetType = typeof(SalesPage) };
            var page3 = new MasterPageItem() { Title = "Account", Icon = "inr", TargetType = typeof(Account) };
            var page4 = new MasterPageItem() { Title = "Purchase", Icon = "purch", TargetType = typeof(PurchasePage) };
            var page5 = new MasterPageItem() { Title = "Human Resource", Icon = "hr", TargetType = typeof(HumanResourcePage) };
            var page6 = new MasterPageItem() { Title = "Approvels", Icon = "thumbs_up", TargetType = typeof(ApprovelsPage) };
            var page8 = new MasterPageItem() { Title = "Setting", Icon = "setting", TargetType = typeof(SettingPage) };
            var page9 = new MasterPageItem() { Title = "Help & Feedback", Icon = "help", TargetType = typeof(HelpPage) };
            var page10 = new MasterPageItem() { Title = "Logout", Icon = "logout", TargetType = typeof(LogOutPage) };

            // Adding menu items to menuList
            menuList.Add(page1);
            menuList.Add(page2);
            menuList.Add(page3);
            menuList.Add(page4);
            menuList.Add(page5);
            menuList.Add(page6);
            menuList.Add(page8);
            menuList.Add(page9);
            menuList.Add(page10);
            // Setting our list to be ItemSource for ListView in MainPage.xaml
            NavigationList.ItemsSource = menuList;
           
                // Initial navigation, this can be used for our home page
               // Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
          
        }

        private async void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
           
            if (item.Title == "Logout")
            {
                await PopupNavigation.PushAsync(new LogOutPage());
            }
            else
            {
                Type page = item.TargetType;
                await App.NavigationDetailPage((Page)Activator.CreateInstance(page));

            }
            App.MasterDetail.IsPresented = false;
            
        }
        
        private void MainPickr_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex == -1) return;
            try
            {
                    
                UserModel res = new UserModel();
                res = StaticMethods.GetLocalSavedData();
                StaticMethods.SetCompanyName = (string)picker.Items[selectedIndex];
                res.CompanyIndex = selectedIndex.ToString();
                StaticMethods.SaveLocalData(res);
            }
            catch (Exception ex)
            {
            }
        }

        //private void C21_Tapped(object sender, EventArgs e)
        //{
        //    if (!(Ti.Text == "C21/MALHAR"))
        //    {
        //        Ti.BackgroundColor = Color.White;
        //        Ti.TextColor = Color.FromHex("#4472C4");
        //        C21.BackgroundColor = Color.FromHex("#4472C4");
        //        C21.TextColor = Color.White;
        //    }
        //}

        //private void TI_Tapped(object sender, EventArgs e)
        //{
        //    if (!(C21.Text == "Ti"))
        //    {
        //        Ti.BackgroundColor = Color.FromHex("#4472C4");
        //        Ti.TextColor = Color.White;
        //        C21.BackgroundColor = Color.White;
        //        C21.TextColor = Color.FromHex("#4472C4");
        //    }
        //}
    }
}