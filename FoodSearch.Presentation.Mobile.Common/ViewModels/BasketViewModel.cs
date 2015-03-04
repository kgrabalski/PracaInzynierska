using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Helpers;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client;
using FoodSearch.Service.Client.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class BasketViewModel : ViewModelBase
    {
        public BasketViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            UpdateBasket();
        }

        private ObservableCollection<BasketItem> _basketItems = new ObservableCollection<BasketItem>();

        public ObservableCollection<BasketItem> BasketItems
        {
            get { return _basketItems; }
            set { SetProperty(ref _basketItems, value); }
        }

        private BasketItem _selectedItem;

        public BasketItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private bool _isEmpty = true;

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { if (SetProperty(ref _isEmpty, value)) IsNotEmpty = !value; }
        }

        private bool _isNotEmpty = false;

        public bool IsNotEmpty
        {
            get { return _isNotEmpty; }
            set { SetProperty(ref _isNotEmpty, value); }
        }

        private string _dishesTotal;

		public string DishesTotal
        {
            get { return _dishesTotal; }
            set { SetProperty(ref _dishesTotal, value); }
        }

		private string _deliveryPrice;

		public string DeliveryPrice
        {
            get { return _deliveryPrice; }
            set { SetProperty(ref _deliveryPrice, value); }
        }

		private string _totalPrice;

		public string TotalPrice
        {
            get { return _totalPrice; }
            set { SetProperty(ref _totalPrice, value); }
        }

        private Command _orderCommand;

        public Command OrderCommand
        {
            get
            {
                return _orderCommand ?? (_orderCommand = new Command(async () =>
                    {
                        if (IsEmpty) 
                        {
                            Services.Dialog.Toast("Koszyk jest pusty");
                            return;
                        }
                        if (!Services.Authorization.IsAuthorized)
                        {
                            Services.Dialog.Confirm(new ConfirmConfig(){
                                Title = "Logowanie",
                                Message = "Aby kontynuować składanie zamówienia musisz być zalogowany.\nCzy chcesz przejść teraz do ekranu logowania?",
                                OkText = "Tak",
                                CancelText = "Nie",
                                OnConfirm = (bool response) => { if (response) Services.Authorization.AuthorizationCommand.Execute(null); }
                            });
                            return;
                        }
                        //TODO: Skladanie zamowienia
                        await Services.Navigation.Navigate.PushAsync(ViewLocator.Order);
                    }));
            }
        }

        private Command _clearBasket;

        public Command ClearBasket
        {
            get
            {
                return _clearBasket ?? (_clearBasket = new Command(async () =>
                    {
                        Loading(true);
						bool cleared = await Client.Order.ClearBasket();
                        if (cleared) 
                        {
                            await UpdateBasketAsync();
                            Services.Dialog.Toast("Wyczyszczono koszyk");
                        }
                        Loading(false);
                    }));
            }
        }

		private Command<BasketItem> _addToBasket;

		public Command<BasketItem> AddToBasket
		{
			get
			{
				return _addToBasket ?? (_addToBasket = new Command<BasketItem>(async item =>
					{
                        Loading(true);
						bool success = await Client.Order.AddToBasket (item.DishId);
						if (success) await UpdateBasketAsync ();
                        Loading(false);
					}));
			}
		}

		private Command<BasketItem> _removeFromBasket;

		public Command<BasketItem> RemoveFromBasket
		{
			get
			{
				return _removeFromBasket ?? (_removeFromBasket = new Command<BasketItem>(async item =>
					{
                        Loading(true);
						bool success = await Client.Order.RemoveFromBasket (item.DishId);
						if (success) await UpdateBasketAsync ();
                        Loading(false);
					}));
			}
		}

        public ICommand LoginCommand { get { return Services.Authorization.AuthorizationCommand; } }

        private async void UpdateBasket()
        {
			await UpdateBasketAsync ();
        }

		private async Task UpdateBasketAsync()
		{
            Loading(true);
            var basketItems = await Client.Order.GetBasket();
            bool isEmpty = basketItems.Count == 0;
            decimal dishesTotal = basketItems.Sum(x => x.Total);
            decimal deliveryPrice = await Client.Order.GetDeliveryPrice(Services.Restaurant.CurrentRestaurantId, dishesTotal);
			decimal totalPrice = dishesTotal + deliveryPrice;
			
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                BasketItems = basketItems;
                IsEmpty = isEmpty;
                DishesTotal = dishesTotal.ToPln();
                DeliveryPrice = deliveryPrice.ToPln();
                TotalPrice = totalPrice.ToPln();
                Services.Dialog.HideLoading();
            });
		}

        private void Loading(bool show)
        {
            if (show)
                Services.Dialog.ShowLoading("Odświeżanie koszyka...");
            else
                Services.Dialog.HideLoading();
        }
    }
}

