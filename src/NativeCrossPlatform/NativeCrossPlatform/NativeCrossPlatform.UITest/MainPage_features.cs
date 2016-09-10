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
