using System;
using System.Windows.Input;

namespace FormsCrossPlatform.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, p => true)
        {
        }

        public RelayCommand(
            Action<object> execute,
            Func<object, bool> canExecute)
        {
            if (null == execute)
                throw new ArgumentNullException(nameof(execute));
            if (null == canExecute)
                throw new ArgumentNullException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) =>
            _canExecute.Invoke(parameter);

        public void Execute(object parameter) =>
            _execute.Invoke(parameter);

        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
