using App2.NativeMathods;
using App2.PopUpPages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpiredSoon : ContentPage
    {
        public List<ShowExpiredSoon> ShowInvoiceList { get; set; }
        public double _Width = 0;

        public ExpiredSoon()
        {
            InitializeComponent();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                    _Width=
                    lblBookDate.WidthRequest = 
                    lblCustName.WidthRequest =  
                    lblSiteName.WidthRequest =
                    lblUnitno.WidthRequest=
                    lblTermDate.WidthRequest = calcScreenWidth / 4 - 30;
                LblExpireTitle.Text =StaticMethods.SetCompanyName;
            }
            ExpireSoonList();
        }

        protected async override void OnAppearing()
        {
            await Loading.ScaleTo(0, 10, Easing.CubicIn);
            await Loading.ScaleTo(1, 1000, Easing.CubicOut);
        }

        public async Task  ExpireSoonList()
        {
            ShowInvoiceList = new List<ShowExpiredSoon>();
            try
            {
                var expireitems = StaticMethods.ExpiredSoon;
                if (expireitems.ExpiredSoonList != null)
                {
                   //var itemsCount= expireitems.ExpiredSoonList.Count.ToString();
                    foreach (var items in expireitems.ExpiredSoonList)
                    {
                        ShowInvoiceList.Add(new ShowExpiredSoon
                        {
                            TxtWidth = _Width,
                            BookngDate = items.BookngDate,
                            BrandName = items.BrandName,
                            CustomerName = items.CustomerName,
                            TerminationDate = items.TerminationDate,
                            UnitNo = items.UnitNo
                        });
                    }
                }
                else
                { 
                        await PopupNavigation.PushAsync(new LoginSuccessPopupPage("E", expireitems.Message));
                }
                ListViewMain.ItemsSource = ShowInvoiceList;
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }

      

        private async void ExpireIcon_Clicked(object sender, EventArgs e)
        {
            string ExpireTag = "Expire";
            PopupTask pt = new PopupTask();
            var result = await pt.OpenMultipleDataInputAlertDialog(ExpireTag);
           await ExpireSoonList();
        }
    }

    public class ShowExpiredSoon
    {
        public double TxtWidth { get; set; }
        public string CustomerName { get; set; }
        public string TerminationDate { get; set; }
        public string BookngDate { get; set; }
        public string BrandName { get; set; }
        public string UnitNo { get; set; }
    }
}