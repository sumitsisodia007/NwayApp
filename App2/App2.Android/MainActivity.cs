
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Firebase.Iid;
using System.Threading.Tasks;
using Android.Content;
using App2.Model;
using App2.NativeMathods;   


namespace App2.Droid
{
    [Activity(  Label = "NwayConstructionERP",
                Icon = "@drawable/icon", 
                Theme = "@style/MainTheme", 
                MainLauncher = true, 
                ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);
            Task.Run(() =>
            {
                var instanceid = FirebaseInstanceId.Instance;
                instanceid.DeleteInstanceId();
            });
            global::Xamarin.Forms.Forms.Init(this, bundle);
        

            var intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
            string tag_type = Intent.GetStringExtra("tag_type");
            string party_id = Intent.GetStringExtra("party_id");
            string msg = Intent.GetStringExtra("msg");

            NavigationMdl mdl = new NavigationMdl();

            mdl.Device_id = StaticMethods.getDeviceidentifier();
            if (mdl.Device_id == "unknown")
            {
                mdl.Device_id = "123456";
            }
            mdl.Company_name = Helper.EnumMaster.C21_MALHAR;
            mdl.Party_id = party_id;

            if (tag_type == "paid")
            {
                mdl.Tag_type = Helper.EnumMaster.TAGTYPEPAYABLE_OUTSTANDING;
                LoadApplication(new App(mdl));
            }
            else if (tag_type == "receipt")
            {
                mdl.Tag_type = Helper.EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING;
                LoadApplication(new App(mdl));
            }
            else
            {
                LoadApplication(new App());
            }
        }

        //private void RegisterWithGCM()
        //{
        //    // Check to ensure everything's set up right
        //    GcmClient.CheckDevice(this);
        //    GcmClient.CheckManifest(this);

        //    // Register for push notifications
        //    Log.Info("MainActivity", "Registering...");
        //    GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
        //}
    }
    
}

