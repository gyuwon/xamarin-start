# 네이티브 크로스 플랫폼

## 프로젝트 생성
[File] -> [New] -> [Project] -> [Visual C#] -> [Cross-Platform] -> [Blank App (Native Portable)]

## TapCounterService

```csharp
namespace NativeCrossPlatform
{
    public class TapCounterService
    {
        private int _count;

        public int Count => _count;

        public string Message => $"{_count} clicks!";

        public void OnTap() => _count++;
    }
}
```

### iOS

```csharp
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
```

### Android

```csharp
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
```

## UI 테스팅

```csharp
using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace NativeCrossPlatform.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class MainPage_features
    {
        private Platform _platform;
        private IApp _app;

        public MainPage_features(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void InitializeApp()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test]
        public void page_has_myButton()
        {
            Func<AppQuery, AppQuery> buttonQuery = q => q.Button("myButton");
            AppResult[] queryResults = _app.Query(buttonQuery);
            Assert.AreEqual(expected: 1, actual: queryResults.Length);
        }

        [Test]
        public void when_button_tapped_twice_then_its_label_changed()
        {
            // Arrange
            Func<AppQuery, AppQuery> buttonQuery = q => q.Button("myButton");

            // Act
            _app.Tap(buttonQuery);
            _app.Tap(buttonQuery);

            // Assert
            AppResult buttonResult = _app.Query(buttonQuery).Single();
            string actual = buttonResult.Text ?? buttonResult.Label;
            string expected = "2 clicks!";
            Assert.AreEqual(expected, actual);
        }
    }
}
```

### iOS

```csharp
using Foundation;
using UIKit;

namespace NativeCrossPlatform.iOS
{
    [Register ("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window {
            get;
            set;
        }

        public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {
#if DEBUG
            Xamarin.Calabash.Start();
#endif

            return true;
        }
    }
}
```

# Xamarin.Forms 크로스 플랫폼

## 프로젝트 생성

[File] -> [New] -> [Project] -> [Visual C#] -> [Cross-Platform] -> [Blank Xaml App (Xamarin.Forms Portable)]
