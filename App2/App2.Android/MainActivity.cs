
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Firebase.Iid;
using System.Threading.Tasks;

namespace App2.Droid
{
    [Activity(  Label = "App2",
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
            Task.Run(() => {
                var instanceid = FirebaseInstanceId.Instance;
                instanceid.DeleteInstanceId();
               // Log.Debug("TAG", "{0} {1}", instanceid.Token, instanceid.GetToken(this.GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
            });
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

