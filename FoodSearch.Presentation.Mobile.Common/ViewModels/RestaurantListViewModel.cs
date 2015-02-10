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
using FoodSearch.Presentation.Mobile.Common.Models;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class RestaurantListViewModel : ViewModelBase
    {
        public RestaurantListViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            Services.Messaging.Register<StreetNumber>(GetRestaurants);
        }

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
                    Services.Navigation.Navigate.PushAsync(ViewLocator.RestaurantMenu);
                    Services.Messaging.Send(SelectedRestaurant);
                    Services.Messaging.Send(new RestaurantId() {Id = SelectedRestaurant.RestaurantId});
                    SelectedRestaurant = null;
                }
            }
        }

        private async void GetRestaurants(StreetNumber sn)
        {
            Services.Dialog.ShowLoading("Wyszukiwanie restauracji...");
            Restaurants = await Client.Core.GetRestaurants(sn.Id);
            Services.Dialog.HideLoading();
        }
    }
}
