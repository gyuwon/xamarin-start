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
