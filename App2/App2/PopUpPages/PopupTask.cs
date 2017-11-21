using App2.APIService;
using App2.Model;
using App2.NativeMathods;
using App2.View;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.PopUpPages
{
    public class PopupTask
    {
        private readonly API _api = new API();
        private NavigationMdl _objNav = null;
        
        public async Task<MyDataModel> OpenMultipleDataInputAlertDialog(string tag)
        {
           var user= StaticMethods.GetLocalSavedData();

           // create the TextInputView
           var inputView = new PopupSettingView(tag,user.SetCancelDays,user.SetExpireDays);
           
            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<MyDataModel>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                async (sender, obj) =>
                {
                    // handle validations
                    if (string.IsNullOrEmpty(((PopupSettingView)sender).MultipleDataResult.DayCancel))
                    {
                        ((PopupSettingView)sender).ValidationLabelText = "Ops! You need to enter the Cancellation Days!";
                        ((PopupSettingView)sender).IsValidationLabelVisible = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(((PopupSettingView)sender).MultipleDataResult.DayExpire))
                    {
                        ((PopupSettingView)sender).ValidationLabelText = "Ops! You need to enter the Expire Days!";
                        ((PopupSettingView)sender).IsValidationLabelVisible = true;
                        return;
                    }
                    if (!string.IsNullOrEmpty(((PopupSettingView)sender).MultipleDataResult.DayCancel) && !string.IsNullOrEmpty(((PopupSettingView)sender).MultipleDataResult.DayExpire))
                    {
                        var loadingPage = new LoaderPage();
                        await PopupNavigation.PushAsync(loadingPage);
                        _objNav = new NavigationMdl();
                        NavigationMdl nav = await _objNav.PrepareApiData();

                        var userdata = StaticMethods.GetLocalSavedData();
                        nav.CancelDayCount =Convert.ToInt32(((PopupSettingView)sender).MultipleDataResult.DayCancel);
                        userdata.SetCancelDays = nav.CancelDayCount.ToString();

                        nav.ExpireDayCount= Convert.ToInt32(((PopupSettingView)sender).MultipleDataResult.DayExpire);
                        userdata.SetExpireDays = nav.ExpireDayCount.ToString();

                        StaticMethods.SaveLocalData(userdata);

                        var msg = await _api.SaveExp_Cancel(nav);
                        if (msg == "Setting Saved !")
                        {
                            await Task.Delay(500);
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
                        await PopupNavigation.RemovePageAsync(loadingPage);
                    }

                    ((PopupSettingView)sender).IsValidationLabelVisible = false;
                    popup.PageClosedTaskCompletionSource.SetResult(((PopupSettingView)sender).MultipleDataResult);
                };

            // subscribe to the TextInputView's Button click event
            inputView.CancelButtonEventHandler +=
                (sender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult(null);
                };

            // Push the page to Navigation Stack
            await PopupNavigation.PushAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await PopupNavigation.PopAsync();

            // return user inserted text value
            return result;
        }
    }
}
