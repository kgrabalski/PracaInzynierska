using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class ViewModelBase : ObservableObject
	{
		protected readonly IFoodSearchServiceClient Client;
        protected readonly IServiceLocator Services;

        public ViewModelBase (IFoodSearchServiceClient client, IServiceLocator serviceLocator)
		{
			Client = client;
            Services = serviceLocator;
		}
	}
}

