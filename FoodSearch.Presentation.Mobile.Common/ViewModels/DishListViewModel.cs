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
using Xamarin.Forms;
using Acr.XamForms.UserDialogs;

using Command = FoodSearch.Presentation.Mobile.Common.Infrastucture.Command;
using FoodSearch.Service.Client.Interfaces;

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

        private Dish _selectedDish;

        public Dish SelectedDish
        {
            get { return _selectedDish; }
            set { SetProperty(ref _selectedDish, value); }
        }

        private Command _addToBasket;

        public Command AddToBasket
        {
            get
            {
                return _addToBasket ?? (_addToBasket = new Command(async () =>
                    {
                        if (SelectedDish != null)
                        {
                            IsBusy = true;
                            bool success = await Client.Order.AddToBasket(SelectedDish.Id);
                            if (success) 
                            {
                                _dialogService.Toast("Dodano do koszyka: " + SelectedDish.Name);
                            }
                            IsBusy = false;
                        } else _dialogService.Toast("Wybierz danie które chcesz dodać do koszyka");
                    }));
            }
        }

        private Command _showBasket;

        public Command ShowBasket
        {
            get
            {
                return _showBasket ?? (_showBasket = new Command(() =>
                    {
                        NavigationService.Navigation.PushAsync(ViewLocator.Basket);
                    }));
            }
        }

        private readonly IUserDialogService _dialogService;

        public DishListViewModel(IFoodSearchServiceClient client, IUserDialogService dialogService) : base(client)
        {
            _dialogService = dialogService;
            MessageService.Register<Restaurant>(GetDishes);
        }

        private async void GetDishes(Restaurant r)
        {
            RestaurantService.CurrentRestaurantId = r.RestaurantId;
            RestaurantService.CurrentRestaurantName = r.RestaurantName;
            IsBusy = true;
            DishGroups = (await Client.Core.GetDishes(r.RestaurantId))
                .Select(x => new Grouping<Dish>(x.Dishes) {GroupName = x.Name})
                .AsObservable();
            IsBusy = false;
        }
    }
}
