using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected bool SetProperty<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
		{
            if (property != null && property.Equals(newValue)) return false;

			property = newValue;
			var handler = PropertyChanged;
			if (handler != null) 
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
			return true;
		}

		protected readonly IFoodSearchServiceClient Client;

		public ViewModelBase (IFoodSearchServiceClient client)
		{
			Client = client;
		}

		public ViewModelBase () : this(new FoodSearchServiceClient())
		{

		}

        private bool _isBusy = false;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

	}
}

