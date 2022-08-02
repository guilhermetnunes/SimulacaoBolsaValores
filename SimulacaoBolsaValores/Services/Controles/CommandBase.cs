using System;
using System.Windows.Input;

namespace AppExemploMVVM.Services
{    
    public class CommandBase : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged;
                
        public CommandBase(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => this._canExecute == null || this._canExecute(parameter);

        public void Execute(object parameter) => this._execute(parameter);
    }
}