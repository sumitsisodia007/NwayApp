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
    public partial class CashFlowPage : ContentPage
    {
        public List<CashFlowDetails> CashFlowDetailses { get; set; }
        public double _Width = 0;
        public CashFlowPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;

                LblSiteName.WidthRequest =
                    LblSiteAmt.WidthRequest =
                        LblSiteBank.WidthRequest =_Width = calcScreenWidth / 4 - 20;
            }
            TodayCollationList();
        }
        public void TodayCollationList()
        {

            CashFlowDetailses = new List<CashFlowDetails>();
            try
            {
                CashFlowDetailses.Add(new CashFlowDetails { TxtWidth = _Width,SiteAmount ="120000", SitesName= "Indore-C21", SiteBank= "HDFC" });
                CashFlowDetailses.Add(new CashFlowDetails { TxtWidth = _Width,SiteAmount ="120000", SitesName= "Ujjain-C21", SiteBank= "SBI" });
                CashFlowDetailses.Add(new CashFlowDetails { TxtWidth = _Width,SiteAmount ="120000", SitesName= "Mandsaur-C21", SiteBank= "BOM" });
                CashFlowDetailses.Add(new CashFlowDetails { TxtWidth = _Width,SiteAmount ="120000", SitesName= "Ratlam-C21", SiteBank = "AXIS" });
                listView.ItemsSource = CashFlowDetailses;
            }
            catch (Exception)
            {


            }
        }

    }

    public class CashFlowDetails
    {
        public string SitesName { get; set; }
        public string SiteBank{ get; set; }
        public string SiteAmount { get; set; }
        public double TxtWidth { get; set; }
    }
}