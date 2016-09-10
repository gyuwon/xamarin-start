using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FormsCrossPlatform.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _name;
        private string _email;

        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();

            Add = new RelayCommand(p =>
            {
                if (string.IsNullOrWhiteSpace(Name) ||
                    string.IsNullOrWhiteSpace(Email))
                    return;

                Contacts.Add(new Contact(Name, Email));

                Name = string.Empty;
                Email = string.Empty;
            });
        }

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        public ObservableCollection<Contact> Contacts { get; }

        public ICommand Add { get; }
    }
}
