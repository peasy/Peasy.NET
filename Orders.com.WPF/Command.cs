using System;
using System.Windows.Input;

namespace Orders.com.WPF
{
    public class Command : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        public Command(Action action)
        {
            _action = action;
        }

        public Command(Action action, Func<bool> canExecute) : this(action)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute();

            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
