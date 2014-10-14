using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.Models;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class DishListViewModel : ViewModelBase
    {
        public DishListViewModel()
        {
            DishGroups = new List<Grouping<Dish>>();
            MessageService.Register<Restaurant>(GetDishes);
        }

        private async void GetDishes(Restaurant r)
        {
            DishGroups = (await Client.Core.GetDishes(r.RestaurantId))
                .Select(x => new Grouping<Dish>(x.Dishes) {GroupName = x.Name});

        }

        #region DishGroups

        private IEnumerable<Grouping<Dish>> _dishGroups;
        public IEnumerable<Grouping<Dish>> DishGroups
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
        
        #region SelectedDish

        private Dish _selecteDish;
        public Dish SelectedDish
        {
            get
            {
                return _selecteDish;
            }
            set
            {
                if (_selecteDish != value)
                {
                    _selecteDish = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
