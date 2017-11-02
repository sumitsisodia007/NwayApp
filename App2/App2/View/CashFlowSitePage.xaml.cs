using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using App2.NativeMathods;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CashFlowSitePage : ContentPage
    {
        public List<CashFlowSite> CashFlowDetailses { get; set; }
        public double _Width = 0;
        public CashFlowSitePage()
        {
            InitializeComponent();
            try
            {
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
                {
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;

                    LblSiteName.WidthRequest =
                    LblSiteBank.WidthRequest = _Width = calcScreenWidth / 2 - 20;
                    LblCashSite.Text = StaticMethods.SetCompanyName;
                }
                BankList();
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        public void BankList()
        {
            CashFlowDetailses = new List<CashFlowSite>();
            try
            {
                var cash = StaticMethods.BankRes;
                foreach (var items in cash.ListCashFlowSite)
                {
                    if (items.CompanyName == StaticMethods.SetCompanyName)
                    {
                        foreach (var collection in items.ListSiteAccountMdls)
                        {
                            CashFlowDetailses.Add(new CashFlowSite
                            {
                                TxtWidth = _Width,
                                SiteTotalAmt = collection.Amt+" "+ collection.AmtType+"   ",
                                SitesName = collection.SiteName,
                            });
                        }
                    }
                }
                ListCashSite.ItemsSource = CashFlowDetailses;
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        private async void ListCashSite_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
          var lstItems= (CashFlowSite)e.Item;
         //   var page = new CashFlowAccountType();
            await Navigation.PushAsync(new CashFlowAccountType(lstItems));
        }
    }
    public class CashFlowSite
    {
        public string SitesName { get; set; }
        public string SiteTotalAmt { get; set; }
        public string AmtType { get; set; }
        public double TxtWidth { get; set; }
    }
}