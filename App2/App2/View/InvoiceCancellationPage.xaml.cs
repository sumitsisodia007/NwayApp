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
    public partial class InvoiceCancellationPage : ContentPage
    {
        public List<ShowInvoiceListMdl> ShowInvoiceList { get; set; }
        public double _Width = 0;
        public InvoiceCancellationPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                _Width =
                    lblCancelDate.WidthRequest =
                    lblCustName.WidthRequest =
                    lblInvoiceCode.WidthRequest = 
                    lblInvoiceType.WidthRequest =
                    lblInvoiceDate.WidthRequest = calcScreenWidth / 4 - 30;
                LblInvoceTitle.Text = StaticMethods.SetCompanyName;
            }
            InvoiceList();
        }
        protected async override void OnAppearing()
        {
            await CancelImg.ScaleTo(0, 10, Easing.CubicIn);
            await CancelImg.ScaleTo(1, 1000, Easing.CubicOut);
        }
        public async void InvoiceList()
        {
            ShowInvoiceList = new List<ShowInvoiceListMdl>();
            try
            {
                var invoicecancel = StaticMethods.InvoiceCancel;
                if (invoicecancel.CancellationList != null)
                {
                    foreach (var items in invoicecancel.CancellationList)
                    {
                        ShowInvoiceList.Add(new ShowInvoiceListMdl
                        {
                            TxtWidth = _Width,
                            CustomerName = items.CustomerName,
                            CancellationDate = items.CancellationDate,
                            InvoiceCode = items.InvoiceCode,
                            InvoiceDate = items.InvoiceDate,
                            InvoiceType = items.InvoiceType
                        });
                    }
                }
                else
                {
                    await PopupNavigation.PushAsync(new LoginSuccessPopupPage("E", invoicecancel.Message));
                }
                InvoceCancellation.ItemsSource = ShowInvoiceList;
            }
            catch (Exception exception)
            {
                StaticMethods.ShowToast(exception.Message);
            }
        }
    }

    public class ShowInvoiceListMdl
    {
        public string CustomerName { get; set; }
        public string CancellationDate { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceCode { get; set; }
        public string InvoiceType { get; set; }
        public double TxtWidth { get; set; }
    }
}