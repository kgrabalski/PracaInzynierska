using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class RestaurantListViewModel : ViewModelBase
    {
        #region Restaurants

        private IEnumerable<Restaurant> _restaurants;
        public IEnumerable<Restaurant> Restaurants
        {
            get
            {
                return _restaurants;
            }
            set
            {
                if (_restaurants != value)
                {
                    _restaurants = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region SelectedRestaurant

        private Restaurant _selectedRestaurant;
        public Restaurant SelectedRestaurant
        {
            get
            {
                return _selectedRestaurant;
            }
            set
            {
                if (_selectedRestaurant != value)
                {
                    _selectedRestaurant = value;
                    OnPropertyChanged();
                    if (_selectedRestaurant != null)
                    {
                        NavigationService.Navigation.PushAsync(ViewLocator.RestaurantMenu);
                        MessageService.Send(SelectedRestaurant);
                    }
                }
            }
        }
        #endregion
        
        public RestaurantListViewModel()
        {
            Restaurants = new List<Restaurant>();
            MessageService.Register<StreetNumber>(GetRestaurants);
        }

        private async void GetRestaurants(StreetNumber sn)
        {
            Restaurants = await Client.Core.GetRestaurants(sn.Id);
        }
    }
}
