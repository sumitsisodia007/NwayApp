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

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace App2.iOS.CustomRenderer
{
    public class CircleViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            Layer.MasksToBounds = true;
            Layer.CornerRadius = (float)((CircleView)Element).CornerRadius / 2.0f;
        }

    }
}