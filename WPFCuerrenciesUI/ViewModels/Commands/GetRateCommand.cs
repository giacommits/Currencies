using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFCuerrenciesUI.ViewModels.Commands
{
    public class GetRateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private Func<Task> _execute;
        private Func<bool> _canExecuteEvaluator;

        public GetRateCommand(Func<Task> execute, Func<bool> canExecuteEvaluator)
        {
            _execute = execute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecuteEvaluator.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
