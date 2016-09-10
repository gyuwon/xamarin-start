using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FormsCrossPlatform.ViewModels
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        protected bool Set<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;

            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
