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
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using System.Windows.Input;

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
                            DialogService.ShowLoading("Dodawanie do koszyka...");
                            bool success = await Client.Order.AddToBasket(SelectedDish.Id);
                            DialogService.HideLoading();
                            if (success) DialogService.Toast("Dodano do koszyka: " + SelectedDish.Name);
                        } else DialogService.Toast("Wybierz danie które chcesz dodać do koszyka");
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

        public ICommand LoginCommand { get { return AuthorizationService.AuthorizationCommand; } }

        public DishListViewModel(IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService) : base(client, authorizationService, dialogService)
        {
            MessageService.Register<Restaurant>(GetDishes);
        }

        private async void GetDishes(Restaurant r)
        {
            RestaurantService.CurrentRestaurantId = r.RestaurantId;
            RestaurantService.CurrentRestaurantName = r.RestaurantName;
            DialogService.ShowLoading("Wyszukiwanie dan...");
            DishGroups = (await Client.Core.GetDishes(r.RestaurantId))
                .Select(x => new Grouping<Dish>(x.Dishes) {GroupName = x.Name})
                .AsObservable();
            await Client.Order.SetCurrentRestaurant(r.RestaurantId);
            DialogService.HideLoading();
        }
    }
}
