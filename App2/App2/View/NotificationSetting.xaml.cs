using App2.APIService;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
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
        NavigationMdl nav = null;
        API api = null;

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

        async Task SettingNotification()
        {
            nav = new NavigationMdl();
            api = new API();

            ResponseModel res = StaticMethods.GetLocalSavedData();

            nav.Tag_type = App2.Helper.EnumMaster.SETTINGS;
            nav.User_id = res.User_Id;
            nav.Device_id = "123";// StaticMethods.getDeviceidentifier();
           res.Min_Receipt_Amt= lblMinAmt.Text = nav.Min_Receipt_Amount = txtMinimumAmt.Text + ".00";
           res.Notification_Day_Count= lblpreDays.Text = nav.Notification_Day_Count = txtDaysOfno.Text;

           string rs = await api.NotificationSetting(nav);
            if (rs == "False")
            {
                StaticMethods.SaveLocalData(res);
                txtDaysOfno.Text = txtMinimumAmt.Text = "";
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("S", "Setting Saved !"));
               // StaticMethods.ShowToast("Setting Saved !");
            }
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResponseModel rs = StaticMethods.GetLocalSavedData();
            lblMinAmt.Text = rs.Min_Receipt_Amt;
            lblpreDays.Text = rs.Notification_Day_Count;

        }
    }
}