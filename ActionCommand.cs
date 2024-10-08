using System;
using System.Windows.Input;

namespace Data_Input_Validation
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canexecute;

        public event EventHandler? CanExecuteChanged;


        public ActionCommand(Action<object> execute, Predicate<object> canexecute)
        {
            this.execute = execute;
            this.canexecute = canexecute;
        }


        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());

        }


        public bool CanExecute(object? parameter)
        {

            return this.canexecute is null || this.canexecute(parameter);
        }

        public void Execute(object? parameter)
        {
            this.execute(parameter);

        }
    }
}
