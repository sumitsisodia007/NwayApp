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
                }
                TodayCollationList();
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        public void TodayCollationList()
        {
            CashFlowDetailses = new List<CashFlowSite>();
            try
            {
                CashFlowDetailses.Add(new CashFlowSite { TxtWidth = _Width, SiteTotalAmt= "160000", SitesName = "C21 BUSINESS PARK" });
                CashFlowDetailses.Add(new CashFlowSite { TxtWidth = _Width, SiteTotalAmt = "1200000", SitesName = "CENTURY 21 TOWN PLANNERS PVT. LTD." });
                ListCashSite.ItemsSource = CashFlowDetailses;
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        private async void ListCashSite_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new CashFlowPage());
        }
    }
    public class CashFlowSite
    {
        public string SitesName { get; set; }
        public string SiteTotalAmt { get; set; }
        public double TxtWidth { get; set; }
    }
}