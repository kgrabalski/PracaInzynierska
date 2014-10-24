using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodSearch.Presentation.Mobile.Common.Infrastucture
{
	public class Command<T> : ICommand
	{
		private readonly Action<T> _executeAction;

		public Command(Action<T> executeAction)
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
                _executeAction((T)parameter);
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

