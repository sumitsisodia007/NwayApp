
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Firebase.Iid;
using System.Threading.Tasks;
using Android.Content;
using App2.Model;


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
            string onclick = Intent.GetStringExtra("onclick");
            string msg = Intent.GetStringExtra("msg");

            NavigationMdl mdl = new NavigationMdl();
           

            mdl.Device_id = "123";
            mdl.Company_name = Helper.EnumMaster.C21_MALHAR;
            mdl.Party_id = party_id;

            if (tag_type == "paid")
            {
                mdl.Tag_type = Helper.EnumMaster.PAYABLE_OUTSTANDING;
                LoadApplication(new App(mdl));
            }
            else if (tag_type == "receipt")
            {
                mdl.Tag_type = Helper.EnumMaster.RECEIVABLE_OUTSTANDING;
                LoadApplication(new App(mdl));
            }
            else
            {
                LoadApplication(new App());
            }
        }
    }
    
}

