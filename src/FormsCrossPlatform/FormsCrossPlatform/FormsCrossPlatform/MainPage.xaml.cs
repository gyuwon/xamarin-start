using FormsCrossPlatform.ViewModels;
using Xamarin.Forms;

namespace FormsCrossPlatform
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel(
                DependencyService.Get<IEventTracker>());
        }
    }
}
