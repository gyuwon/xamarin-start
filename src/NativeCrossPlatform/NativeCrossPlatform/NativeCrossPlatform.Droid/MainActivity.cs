using Android.App;
using Android.OS;
using Android.Widget;

namespace NativeCrossPlatform.Droid
{
    [Activity(Label = "NativeCrossPlatform.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly TapCounterService _service = new TapCounterService();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                _service.OnTap();
                button.Text = _service.Message;
            };
        }
    }
}
