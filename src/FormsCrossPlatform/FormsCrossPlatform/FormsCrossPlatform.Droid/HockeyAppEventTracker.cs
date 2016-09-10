using FormsCrossPlatform.Droid;
using FormsCrossPlatform.ViewModels;
using HockeyApp.Android.Metrics;
using Xamarin.Forms;

[assembly:Dependency(typeof(HockeyAppEventTracker))]

namespace FormsCrossPlatform.Droid
{
    public class HockeyAppEventTracker : IEventTracker
    {
        public void TrackEvent(string eventName)
        {
            MetricsManager.TrackEvent(eventName);
        }
    }
}
