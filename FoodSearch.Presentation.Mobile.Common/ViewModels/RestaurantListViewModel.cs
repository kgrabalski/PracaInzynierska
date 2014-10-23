using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;
using System.Collections.ObjectModel;
using FoodSearch.Service.Client.Interfaces;

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
                 
        public RestaurantListViewModel(IFoodSearchServiceClient client) : base(client)
        {
            MessageService.Register<StreetNumber>(GetRestaurants);
        }

        private async void GetRestaurants(StreetNumber sn)
        {
            IsBusy = true;
            Restaurants = await Client.Core.GetRestaurants(sn.Id);
            IsBusy = false;
        }
    }
}
