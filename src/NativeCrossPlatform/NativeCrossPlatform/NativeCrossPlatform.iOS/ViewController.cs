using System;
using UIKit;

namespace NativeCrossPlatform.iOS
{
    public partial class ViewController : UIViewController
    {
        private readonly TapCounterService _service = new TapCounterService();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Button.AccessibilityIdentifier = "myButton";

            Button.TouchUpInside += delegate
            {
                _service.OnTap();
                Button.SetTitle(_service.Message, UIControlState.Normal);
            };
        }
    }
}
