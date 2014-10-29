using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;
using System.Collections.ObjectModel;
using FoodSearch.Service.Client.Interfaces;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class RestaurantListViewModel : ViewModelBase
    {
        private ObservableCollection<Restaurant> _restaurants = new ObservableCollection<Restaurant>();

        public ObservableCollection<Restaurant> Restaurants
        {
            get { return _restaurants; }
            set { SetProperty(ref _restaurants, value); }
        }

        private Restaurant _selectedRestaurant = new Restaurant();

        public Restaurant SelectedRestaurant
        {
            get { return _selectedRestaurant; }
            set 
            { 
                if (SetProperty(ref _selectedRestaurant, value) && value != null) {
                    NavigationService.Navigation.PushAsync(ViewLocator.RestaurantMenu);
                    MessageService.Send(SelectedRestaurant);
                    SelectedRestaurant = null;
                }
            }
        }
                 
        public RestaurantListViewModel(IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService) : base(client, authorizationService, dialogService)
        {
            MessageService.Register<StreetNumber>(GetRestaurants);
        }

        private async void GetRestaurants(StreetNumber sn)
        {
            DialogService.ShowLoading("Wyszukiwanie restauracji...");
            Restaurants = await Client.Core.GetRestaurants(sn.Id);
            DialogService.HideLoading();
        }
    }
}
