using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Interfaces;

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

