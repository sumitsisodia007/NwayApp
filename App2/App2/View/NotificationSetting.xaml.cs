using App2.APIService;
using App2.Model;
using App2.NativeMathods;
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
          
            nav.Tag_type = "settings";
            nav.User_id = res.User_Id;
            nav.Device_id = "123";
            nav.Min_Receipt_Amount = txtMinimumAmt.Text;
            nav.Notification_Day_Count = txtDaysOfno.Text;

            res = await api.NotificationSetting(nav);
           
            //StaticMethods.ShowToast("");
           
        }

    }
}