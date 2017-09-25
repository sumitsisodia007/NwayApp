using App2.APIService;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
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
    public partial class NotificationSetting : ContentPage
    {
        private NavigationMdl nav = null;
        private API api = null;

        public NotificationSetting()
        {
            InitializeComponent();
        }

        private async void TapOnSave(object sender, EventArgs e)
        {
            _Loading.IsRunning = true;
            await SettingNotification();
            _Loading.IsRunning = false;
        }

        private async Task SettingNotification()
        {
            string rs = null;
            nav = new NavigationMdl();
            api = new API();

            ResponseModel res = StaticMethods.GetLocalSavedData();

            nav.TagType = App2.Helper.EnumMaster.TagtypeSettings;
            nav.UserId = res.UserId;
            nav.DeviceId = StaticMethods.GetDeviceidentifier(); //"123";//
            if (nav.DeviceId == "unknown")
            {
                nav.DeviceId = "123456";
            }
            res.MinReceiptAmt= lblMinAmt.Text = nav.MinReceiptAmount = txtMinimumAmt.Text + ".00";
           res.NotificationDayCount= lblpreDays.Text = nav.NotificationDayCount = txtDaysOfno.Text;
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {
                rs = await api.NotificationSetting(nav);
                if (rs == "False")
                {
                    StaticMethods.SaveLocalData(res);
                    txtDaysOfno.Text = txtMinimumAmt.Text = "";
                    await Navigation.PushPopupAsync(new LoginSuccessPopupPage("S", "Setting Saved !"));
                   // StaticMethods.ShowToast("Setting Saved !");
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResponseModel rs = StaticMethods.GetLocalSavedData();
            lblMinAmt.Text = rs.MinReceiptAmt;
            lblpreDays.Text = rs.NotificationDayCount;

        }
    }
}