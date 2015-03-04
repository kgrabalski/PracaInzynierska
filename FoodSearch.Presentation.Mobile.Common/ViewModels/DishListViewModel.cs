using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Models;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Command = FoodSearch.Presentation.Mobile.Common.Infrastucture.Command;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class DishListViewModel : ViewModelBase
    {
        public DishListViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            Services.Messaging.Register<Restaurant>(GetDishes);
        }

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
                            Services.Dialog.ShowLoading("Dodawanie do koszyka...");
                            bool success = await Client.Order.AddToBasket(SelectedDish.Id);
                            Services.Dialog.HideLoading();
                            if (success) Services.Dialog.Toast("Dodano do koszyka: " + SelectedDish.Name);
                        } else Services.Dialog.Toast("Wybierz danie które chcesz dodać do koszyka");
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
                        Services.Navigation.Navigate.PushAsync(ViewLocator.Basket);
                    }));
            }
        }

        public ICommand LoginCommand { get { return Services.Authorization.AuthorizationCommand; } }

        private async void GetDishes(Restaurant r)
        {
            Services.Restaurant.CurrentRestaurantId = r.RestaurantId;
            Services.Restaurant.CurrentRestaurantName = r.RestaurantName;
            Services.Dialog.ShowLoading("Wyszukiwanie dań...");
            DishGroups = (await Client.Core.GetDishes(r.RestaurantId))
                .Select(x => new Grouping<Dish>(x.Dishes) {GroupName = x.Name})
                .AsObservable();
            await Client.Order.SetCurrentRestaurant(r.RestaurantId);
            Services.Dialog.HideLoading();
        }
    }
}
