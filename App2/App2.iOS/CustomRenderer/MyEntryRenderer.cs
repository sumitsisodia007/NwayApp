using UIKit;
using App2.CustomRenderer;
using Xamarin.Forms;
using App2.iOS.CustomRenderer;
using Xamarin.Forms.Platform.iOS;
using App2.View;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace App2.iOS.CustomRenderer
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.Clear;
                Control.Layer.BorderWidth = 0;
                //Control.Layer.CornerRadius = 5;       //this is for rounded corner
                Control.BorderStyle = UITextBorderStyle.None;
                Control.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
                Control.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
                Control.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization
                if (PayablePage.Flag == 1)
                    Control.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}