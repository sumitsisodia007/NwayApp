
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Firebase.Iid;
using System.Threading.Tasks;
using Android.Content;
using App2.Model;
using App2.NativeMathods;
using Android.Views;

using System.Linq;

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

            mdl.DeviceId = StaticMethods.GetDeviceidentifier();
            if (mdl.DeviceId == "unknown")
            {
                mdl.DeviceId = "123456";
            }
            mdl.CompanyName = Helper.EnumMaster.C21Malhar;
            mdl.PartyId = party_id;

            if (tag_type == "paid")
            {
                mdl.TagType = Helper.EnumMaster.TagtypepayableOutstanding;
                LoadApplication(new App(mdl));
            }
            else if (tag_type == "receipt")
            {
                mdl.TagType = Helper.EnumMaster.TagtypereceivableOutstanding;
                LoadApplication(new App(mdl));
            }
            else
            {
                LoadApplication(new App());
            }
           }
        }
}

