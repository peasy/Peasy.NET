using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orders.com.WPF
{
    public class Command : ICommand
    {
        public Command(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        private Action _action;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
