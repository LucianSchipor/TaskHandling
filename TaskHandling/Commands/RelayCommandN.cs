using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskHandling.Commands
{
    class RelayCommandN<T> : ICommand
    {
        private Action<T> commandTask;

        public RelayCommandN(Action<T> workToDo)
        {
            commandTask = workToDo;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            commandTask((T)parameter);
        }
    }
}
