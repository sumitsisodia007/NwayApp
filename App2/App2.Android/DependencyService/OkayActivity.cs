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

namespace App2.Droid.DependencyService
{
    [Activity(Label = "OkayActivity")]
    public class OkayActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            var intent = new Intent(Android.App.Application.Context, typeof(OkayActivity));
            //intent.PutExtra("title", Intent.GetStringExtra("title"));
            string tag_type = Intent.GetStringExtra("tag_type");
            string party_id = Intent.GetStringExtra("party_id");
            string onclick = Intent.GetStringExtra("onclick");
            string msg = Intent.GetStringExtra("msg");
            
          //  App.Current.MainPage.Navigation.PushAsync(new PayableChart());
        }
    }
}