using App2.Model;
using App2.PopUpPages;
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
        public MasterPage()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            DrawalMenu();
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
                {
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;
                    TI.WidthRequest = C21.WidthRequest = calcScreenWidth / 3;
                }
            });
        }

        public void DrawalMenu()
        {
            menuList = new List<MasterPageItem>();

            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            var page1 = new MasterPageItem() { Title = "Home", Icon = "home", TargetType = typeof(HomePage) };
            var page2 = new MasterPageItem() { Title = "Sales", Icon = "sales", TargetType = typeof(HomePage) };
            var page3 = new MasterPageItem() { Title = "Account", Icon = "inr", TargetType = typeof(Account) };
            var page4 = new MasterPageItem() { Title = "Purchase", Icon = "purch", TargetType = typeof(PurchasePage) };
            var page5 = new MasterPageItem() { Title = "Human Resource", Icon = "hr", TargetType = typeof(HomePage) };
            var page6 = new MasterPageItem() { Title = "Approvels", Icon = "thumbs_up", TargetType = typeof(HomePage) };
            var page8 = new MasterPageItem() { Title = "Setting", Icon = "setting", TargetType = typeof(SettingPage) };
            var page9 = new MasterPageItem() { Title = "Help & Feedback", Icon = "help", TargetType = typeof(LoginPage) };
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
            try
            {
                // Initial navigation, this can be used for our home page
               // Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
            }
            catch (Exception ex)
            {
            }
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

        private void C21_Tapped(object sender, EventArgs e)
        {
            if (!(TI.Text == "C21/MALHAR"))
            {
                TI.BackgroundColor = Color.White;
                TI.TextColor = Color.FromHex("#4472C4");
                C21.BackgroundColor = Color.FromHex("#4472C4");
                C21.TextColor = Color.White;
            }
        }

        private void TI_Tapped(object sender, EventArgs e)
        {
            if (!(C21.Text == "TI"))
            {
                TI.BackgroundColor = Color.FromHex("#4472C4");
                TI.TextColor = Color.White;
                C21.BackgroundColor = Color.White;
                C21.TextColor = Color.FromHex("#4472C4");
            }
        }
    }
}