using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;

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
        protected readonly IAuthorizationService AuthorizationService;
        protected readonly IUserDialogService DialogService;

        public ViewModelBase (IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService)
		{
			Client = client;
            AuthorizationService = authorizationService;
            DialogService = dialogService;
		}
	}
}

