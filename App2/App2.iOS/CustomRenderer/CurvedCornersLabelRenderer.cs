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

[assembly: ExportRenderer(typeof(CurvedCornersLabel), typeof(CurvedCornersLabelRenderer))]
namespace App2.iOS.CustomRenderer
{
    public class CurvedCornersLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var _xfViewReference = (CurvedCornersLabel)Element;
                this.Layer.CornerRadius = (float)_xfViewReference.CurvedCornerRadius;
                this.Layer.BackgroundColor = _xfViewReference.CurvedBackgroundColor.ToCGColor();
            }
        }
    }
}