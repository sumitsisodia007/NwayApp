using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using App2.iOS.CustomRenderer;
using Xamarin.Forms.Platform.iOS;
using App2.CustomRenderer;

[assembly: ExportRenderer(typeof(MyButton), typeof(MyButtonRenderer))]
namespace App2.iOS.CustomRenderer
{
   public class MyButtonRenderer: ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BackgroundColor = UIColor.Clear;
                Control.Layer.BorderWidth = 0;
            }
        }
    }
}