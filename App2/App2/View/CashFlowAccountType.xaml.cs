using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.NativeMathods;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CashFlowAccountType : ContentPage
    {
        public List<CashFlowAccountTypeMdl> AccountTypeDetailses { get; set; }
        public double _Width = 0;

        public CashFlowAccountType(CashFlowSite Items)
        {
            InitializeComponent();
           LblAccType.Text= Items.SitesName;
            try
            {
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
                {
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;

                    LblSiteName.WidthRequest =
                        LblSiteBank.WidthRequest = _Width = calcScreenWidth / 2 - 20;
                }
                TodayCollationList(Items);
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        public void TodayCollationList(CashFlowSite cfItems)
        {
            AccountTypeDetailses = new List<CashFlowAccountTypeMdl>();
            try
            {
                var cash = StaticMethods.BankRes;
                foreach (var items in cash.ListCashFlowSite)
                {
                    foreach (var SitesDetailes in items.ListSiteAccountMdls)
                    {
                        if (SitesDetailes.SiteName == cfItems.SitesName)
                        {
                            foreach (var accountType in SitesDetailes.ListSiteAccountTypeMdls)
                            {
                                AccountTypeDetailses.Add(new CashFlowAccountTypeMdl
                                {
                                    TxtWidth = _Width,
                                    TotalAmt = accountType.Amt+" "+accountType.AmtType+"   ",
                                    AccountType = accountType.AccountHeadName
                                });
                            }
                        }
                    }
                }
                ListCashSite.ItemsSource = AccountTypeDetailses;
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        private async void ListCashAccount_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item=(CashFlowAccountTypeMdl)e.Item;
            await Navigation.PushAsync(new CashFlowPage(item));
        }
    }
    public class CashFlowAccountTypeMdl
    {
        public string AccountType { get; set; }
        public string TotalAmt { get; set; }
        public double TxtWidth { get; set; }
    }
}