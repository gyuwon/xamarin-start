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

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:FormsCrossPlatform"
             x:Class="FormsCrossPlatform.MainPage">

  <Grid>
    <Grid.RowDefinitions>
      <RowDefinition Height="Auto" />
      <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <Grid>
      <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="*" />
      </Grid.ColumnDefinitions>

      <Label Text="Name: " />
      <Entry Grid.Column="1" />

      <Label Grid.Row="1" Text="Email: " />
      <Entry Grid.Row="1" Grid.Column="1" />

      <Button Grid.Row="2" Grid.ColumnSpan="2" Text="Add" />
    </Grid>

    <ListView Grid.Row="1" BackgroundColor="#dfdfdf" />
  </Grid>

</ContentPage>
```

## `Device.OnPlatform<T>`

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:FormsCrossPlatform"
             x:Class="FormsCrossPlatform.MainPage">
  
  <ContentPage.Padding>
    <OnPlatform x:TypeArguments="Thickness" iOS="0,20,0,0" />
  </ContentPage.Padding>

  <Grid>
    <Grid.RowDefinitions>
      <RowDefinition Height="Auto" />
      <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <Grid>
      <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="*" />
      </Grid.ColumnDefinitions>

      <Label Text="Name: " />
      <Entry Grid.Column="1" />

      <Label Grid.Row="1" Text="Email: " />
      <Entry Grid.Row="1" Grid.Column="1" />

      <Button Grid.Row="2" Grid.ColumnSpan="2" Text="Add" />
    </Grid>

    <ListView Grid.Row="1" BackgroundColor="#dfdfdf" />
  </Grid>

</ContentPage>
```

##  응용프로그램 논리

```csharp
using System;

namespace FormsCrossPlatform
{
    public class Contact
    {
        public Contact(string name, string email)
        {
            if (null == name)
                throw new ArgumentNullException(nameof(name));
            if (null == email)
                throw new ArgumentNullException(nameof(email));

            Name = name;
            Email = email;
        }

        public string Name { get; }

        public string Email { get; }
    }
}
```

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:FormsCrossPlatform"
             x:Class="FormsCrossPlatform.MainPage">
  
  <ContentPage.Padding>
    <OnPlatform x:TypeArguments="Thickness" iOS="0,20,0,0" />
  </ContentPage.Padding>

  <Grid>
    <Grid.RowDefinitions>
      <RowDefinition Height="Auto" />
      <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <Grid>
      <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="*" />
      </Grid.ColumnDefinitions>

      <Label Text="Name: " />
      <Entry x:Name="_name" Grid.Column="1" />

      <Label Grid.Row="1" Text="Email: " />
      <Entry x:Name="_email" Grid.Row="1" Grid.Column="1" />

      <Button x:Name="_add" Grid.Row="2" Grid.ColumnSpan="2" Text="Add" />
    </Grid>

    <ListView x:Name="_contactList" Grid.Row="1" BackgroundColor="#dfdfdf">
      <ListView.ItemTemplate>
        <DataTemplate>
          <ViewCell>
            <Grid>
              <Label Text="{Binding Name}" />
              <Label Grid.Column="1" Text="{Binding Email}" />
            </Grid>
          </ViewCell>
        </DataTemplate>
      </ListView.ItemTemplate>
    </ListView>
  </Grid>

</ContentPage>
```

```csharp
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FormsCrossPlatform
{
    public partial class MainPage : ContentPage
    {
        private readonly ObservableCollection<Contact> _contacts;

        public MainPage()
        {
            InitializeComponent();

            _contacts = new ObservableCollection<Contact>();

            _contactList.ItemsSource = _contacts;

            _add.Clicked += delegate
            {
                if (string.IsNullOrWhiteSpace(_name.Text) ||
                    string.IsNullOrWhiteSpace(_email.Text))
                    return;

                _contacts.Add(new Contact(_name.Text, _email.Text));

                _name.Text = string.Empty;
                _email.Text = string.Empty;
            };
        }
    }
}
```
