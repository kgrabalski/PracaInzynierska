using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.Models;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;
using System.Collections.ObjectModel;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class DishListViewModel : ViewModelBase
    {
        private ObservableCollection<Grouping<Dish>> _dishGroups = new ObservableCollection<Grouping<Dish>>();

        public ObservableCollection<Grouping<Dish>> DishGroups
        {
            get { return _dishGroups; }
            set { SetProperty(ref _dishGroups, value); }
        }

        public DishListViewModel()
        {
            MessageService.Register<Restaurant>(GetDishes);
        }

        private async void GetDishes(Restaurant r)
        {
            IsBusy = true;
            DishGroups = (await Client.Core.GetDishes(r.RestaurantId))
                .Select(x => new Grouping<Dish>(x.Dishes) {GroupName = x.Name})
                .AsObservable();
            IsBusy = false;
        }
    }
}
