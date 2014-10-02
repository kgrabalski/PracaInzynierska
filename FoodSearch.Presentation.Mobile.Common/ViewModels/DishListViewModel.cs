using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class DishListViewModel : ViewModelBase
    {
        public DishListViewModel()
        {
            DishGroups = new List<DishGroup>();
            MessageService.Register<Restaurant>(GetDishes);
        }

        private async void GetDishes(Restaurant r)
        {
            await DialogService.Alert(r.RestaurantName, r.RestaurantId.ToString());

        }

        #region DishGroups

        private IEnumerable<DishGroup> _dishGroups;
        public IEnumerable<DishGroup> DishGroups
        {
            get
            {
                return _dishGroups;
            }
            set
            {
                if (_dishGroups != value)
                {
                    _dishGroups = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
