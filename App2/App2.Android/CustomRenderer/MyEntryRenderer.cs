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
using Xamarin.Forms;
using App2.CustomRenderer;
using App2.Droid.CustomRenderer;
using Xamarin.Forms.Platform.Android;
using App2.View;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace App2.Droid.CustomRenderer
{
    class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
               if (PayablePage.flag == 1)
                    Control.Gravity = Android.Views.GravityFlags.Center;
            }
            /* if (Control != null)
                {
                GradientDrawable gd = new GradientDrawable();
                gd.SetCornerRadius(10); // increase or decrease to changes the corner look
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                }
            */
        }
    }
}