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
    public partial class CashFlowPage : ContentPage
    {
        public List<CashFlowDetails> CashFlowDetailses { get; set; }
        public double _Width = 0;
        public CashFlowPage(CashFlowAccountTypeMdl item)
        {
            InitializeComponent();
            LblAccGroup.Text = item.AccountType;
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;

                    LblSiteAmt.WidthRequest =
                        LblSiteBank.WidthRequest = _Width = calcScreenWidth / 4 - 20;
            }
            TodayCollationList(item);
        }
        public void TodayCollationList(CashFlowAccountTypeMdl cfAccountTypeMdl)
        {
            CashFlowDetailses = new List<CashFlowDetails>();
            try
            {
                var cash = StaticMethods.BankRes;
                foreach (var items in cash.ListCashFlowSite)
                {
                    foreach (var SitesDetailes in items.ListSiteAccountMdls)
                    {
                        foreach (var accountType in SitesDetailes.ListSiteAccountTypeMdls)
                        {
                            if (accountType.AccountHeadName == cfAccountTypeMdl.AccountType)
                            {
                                foreach (var bankDetails in accountType.ListSiteAccountBankMdl)
                                {
                                    CashFlowDetailses.Add(new CashFlowDetails
                                    {
                                        TxtWidth = _Width,
                                        SiteAmount = bankDetails.Amt+" "+bankDetails.AmtType+"   ",
                                        SitesName = bankDetails.AccountId.ToString(),
                                        SiteBank = bankDetails.AccountName
                                    });
                                }
                            }
                        }
                    }
                }
                
                listView.ItemsSource = CashFlowDetailses;
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            CashFlowDetails bankdetails = (CashFlowDetails)e.Item;
            LabelAmt.IsVisible = true;
            BankImage(bankdetails.SiteBank, bankdetails.SiteAmount);
        }

        private void BankImage(string bankname,string amt)
        {
            switch (bankname)
            {
                case "ICICI":
                    bankname = "http://www.eazyhomepage.com/Bankicons/private/ICICI_Bank.jpg";
                    break;
                case "HDFC":
                    bankname = "https://qph.ec.quoracdn.net/main-qimg-bf85560f3dd7ddaddd5e48f1463c244b";
                    break;
                case "SBI":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/sbi-logo.png";
                    break;
                case "BOM":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/bank-of-maharashtra.gif";
                    break;
                case "AXIS":
                    bankname = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTXxehX6xnc3qE1AuAsh5vndf6InRJ7LXK9l9EWcxtVSLBhc8j6";
                    break;
                case "BOI":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/bank-of-india.jpg";
                    break;
                case "OBC":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/orientalbankofcomm_1086182f.jpg";
                    break;
                case "UNION":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/union.jpg";
                    break;
                case "ALLAH":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/allahabad-bank-exam-results.png";
                    break;
                case "UBOI":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/united-bank-of-india_thumb.jpg";
                    break;
                case "UCO":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/uco-bank-logo.jpg";
                    break;
                case "SYND":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/syndicate_bank_4037409.jpg";
                    break;
                case "PNB":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/punjab-national-bank-pnb.jpg";
                    break;
                case "IDBI":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/idbi-bank.jpg";
                    break;
                case "CORP":
                    bankname = "https://vanihegde.files.wordpress.com/2013/06/corp.jpg";
                    break;
            }
            //ImageBank.Source = bankname;
           // LabelAmt.Text = amt;
            Spanamt.Text = "  "+amt;
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