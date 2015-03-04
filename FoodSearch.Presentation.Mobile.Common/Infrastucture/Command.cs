using System;
using System.Windows.Input;

namespace FoodSearch.Presentation.Mobile.Common.Infrastucture
{
    public class Command : ICommand
    {
        private readonly Action _executeAction;

        public Command(Action executeAction)
        {
            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_executeAction != null)
            {
                _executeAction();
            }
        }

        public event EventHandler CanExecuteChanged;

		public void OnCanEvecuteChanged()
		{
			var handler = CanExecuteChanged;
			if (handler != null) CanExecuteChanged (this, EventArgs.Empty);
		}
    }
}
