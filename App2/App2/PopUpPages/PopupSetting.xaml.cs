using App2.APIService;
using App2.Model;
using App2.NativeMathods;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupSetting : PopupPage
    {
        private readonly API _api = new API();
        private NavigationMdl _objNav = null;
        string tag = null;
        public PopupSetting(string title1Text)
        {
            InitializeComponent();
            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            tag = title1Text;
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {

            if (!CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {
                var loadingPage = new LoaderPage();
                await PopupNavigation.PushAsync(loadingPage);
                _objNav = new NavigationMdl();
                NavigationMdl nav = await _objNav.PrepareApiData();
                
                nav.CancelDayCount = Convert.ToInt32(txtCancel.Text);
                nav.ExpireDayCount = Convert.ToInt32(txtExpire.Text);
                try
                {
                    if (nav.CancelDayCount == 0)
                    {
                        ValidationLabel.Text = "Ops! You need to enter the Cancellation Days!";
                        ValidationLabel.IsVisible = true;
                    }
                    else if (nav.ExpireDayCount == 0)
                    {
                        ValidationLabel.Text = "Ops! You need to enter the Expire Days!";
                        ValidationLabel.IsVisible = true;
                    }
                    else if (nav.ExpireDayCount == 0 && nav.CancelDayCount == 0)
                    {
                        ValidationLabel.Text = "Ops! You need to enter the Expire and Cancellation Days!";
                        ValidationLabel.IsVisible = true;
                    }
                    else if (nav.ExpireDayCount != 0 && nav.CancelDayCount != 0)
                    {
                        var tempdata = await _api.SaveExp_Cancel(nav);
                        if (tempdata == "Setting Saved !")
                        {
                            if (tag != "Expire")
                            {
                                var invoiceCancal = await _api.InvoiceCancellation(nav);
                                if (invoiceCancal.Error == false)
                                {
                                    StaticMethods.InvoiceCancel = invoiceCancal;
                                }
                            }
                            else
                            {
                                var expiredSoon = await _api.ExpiredSoon(nav);
                                if (expiredSoon.Error == false)
                                {
                                    StaticMethods.ExpiredSoon = expiredSoon;
                                }
                            }
                        }
                        await PopupNavigation.PopAsync(true);
                    }
                    await PopupNavigation.RemovePageAsync(loadingPage);
                    
                }
                catch (Exception ex)
                {
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", ex.Message));
                    await PopupNavigation.RemovePageAsync(loadingPage);
                }
            }

        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {

            PopupNavigation.PopAsync(true);
        }
    }
}