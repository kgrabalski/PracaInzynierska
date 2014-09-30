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

		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		protected readonly IFoodSearchServiceClient _client;

		public ViewModelBase (IFoodSearchServiceClient client)
		{
			_client = client;
		}

		public ViewModelBase () : this(new FoodSearchServiceClient())
		{

		}
	}
}

