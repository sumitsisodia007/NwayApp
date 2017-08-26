using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App2.Model;
using App2.View;
using Xamarin.Forms;

namespace App2.Droid.DependencyService
{
    [Activity(Label = "OkayActivity")]
    public class OkayActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
           
            var intent = new Intent(Android.App.Application.Context, typeof(OkayActivity));
            string tag_type = Intent.GetStringExtra("tag_type");
            string party_id = Intent.GetStringExtra("party_id");
            string onclick  = Intent.GetStringExtra("onclick");
            string msg      = Intent.GetStringExtra("msg");
            
            NavigationMdl mdl = new NavigationMdl();
            if (tag_type == "paid")
            {
                mdl.Tag_type =Helper.EnumMaster.TAGTYPEPAYABLE_OUTSTANDING;
            }
            else if (tag_type== "receipt")
            {
                mdl.Tag_type = Helper.EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING;
            }

            mdl.Device_id = "123";
            mdl.Company_name = Helper.EnumMaster.C21_MALHAR;
            mdl.Party_id = party_id;

            var navPage = new NavigationPage(new LoginPage());
            App.Current.MainPage = navPage;

            //  await navPage.Navigation.PushAsync(new App2.View.PayableChart());
            try
            {
                //await navPage.Navigation.PushAsync(new App2.View.PayableChart()); Not Working
               // LoadApplication(new App(mdl));
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}