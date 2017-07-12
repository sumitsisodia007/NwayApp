using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using App2.CustomRenderer;
using App2.iOS.CustomRenderer;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderEntry), typeof(BorderEntryRenderer))]
namespace App2.iOS.CustomRenderer
{
    public class BorderEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.Clear;
                Control.Layer.BorderWidth = 1;
                Control.Layer.BorderColor = new CoreGraphics.CGColor(red: 0f, green: 0f, blue: 0f, alpha: 0f);
                Control.SpellCheckingType = UITextSpellCheckingType.No;
                Control.AutocorrectionType = UITextAutocorrectionType.No;
                Control.AutocapitalizationType = UITextAutocapitalizationType.None;
                //if (ForgotPasswordPage.flag == 1)
                //    Control.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}