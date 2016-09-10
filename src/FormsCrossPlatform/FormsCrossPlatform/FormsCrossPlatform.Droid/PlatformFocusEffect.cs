using System.ComponentModel;
using FormsCrossPlatform;
using FormsCrossPlatform.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName(nameof(FormsCrossPlatform))]
[assembly: ExportEffect(typeof(PlatformFocusEffect), nameof(FocusEffect))]

namespace FormsCrossPlatform.Droid
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
                Control.SetBackgroundColor(
                    entry.IsFocused
                    ? Android.Graphics.Color.Yellow
                    : Android.Graphics.Color.White);
            }
        }
    }
}
