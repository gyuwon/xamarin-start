using System.ComponentModel;
using FormsCrossPlatform;
using FormsCrossPlatform.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ResolutionGroupName(nameof(FormsCrossPlatform))]
[assembly:ExportEffect(typeof(PlatformFocusEffect), nameof(FocusEffect))]

namespace FormsCrossPlatform.iOS
{
    public class PlatformFocusEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(
            PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == nameof(Entry.IsFocused))
            {
                var entry = (Entry)Element;
                Control.BackgroundColor =
                    entry.IsFocused
                    ? UIColor.Yellow
                    : UIColor.White;
            }
        }
    }
}
