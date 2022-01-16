using Foundation;
using FullSteamAheadMVP0.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(ContentPage), typeof(CustomPageRenderer))]
namespace FullSteamAheadMVP0.iOS
{
    public class CustomPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (UIDevice.CurrentDevice.CheckSystemVersion(13,0))
                OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        }
    }
}