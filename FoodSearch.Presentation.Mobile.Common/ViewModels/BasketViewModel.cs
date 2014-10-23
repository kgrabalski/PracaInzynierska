using System;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client;
using System.Collections.ObjectModel;
using FoodSearch.Service.Client.Interfaces;
using System.Linq;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services;
using Acr.XamForms.UserDialogs;

namespace FoodSearch.Presentation.Mobile.Common
{
    public class BasketViewModel : ViewModelBase
    {
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

        private decimal _dishesTotal;

        public decimal DishesTotal
        {
            get { return _dishesTotal; }
            set { SetProperty(ref _dishesTotal, value); }
        }

        private decimal _deliveryPrice;

        public decimal DeliveryPrice
        {
            get { return _deliveryPrice; }
            set { SetProperty(ref _deliveryPrice, value); }
        }

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { SetProperty(ref _totalPrice, value); }
        }

        private Command _orderCommand;

        public Command OrderCommand
        {
            get
            {
                return _orderCommand ?? (_orderCommand = new Command(() =>
                    {
                        if (IsEmpty) 
                        {
                            _dialogService.Toast("Koszyk jest pusty");
                            return;
                        }
                        //TODO: Skladanie zamowienia
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
                        IsBusy = true;
                        if (await Client.Order.ClearBasket()) 
                        {
                            UpdateBasket();
                            _dialogService.Toast("Wyczyszczono koszyk");
                        }
                        IsBusy = false;
                    }));
            }
        }

        private readonly IUserDialogService _dialogService;

        public BasketViewModel(IFoodSearchServiceClient client, IUserDialogService dialogService) : base(client)
        {
            _dialogService = dialogService;
            UpdateBasket();
        }

        private async void UpdateBasket()
        {
            IsBusy = true;
            BasketItems.Clear();
            BasketItems = await Client.Order.GetBasket();
            IsEmpty = BasketItems.Count == 0;
            DishesTotal = BasketItems.Sum(x => x.Total);
            DeliveryPrice = await Client.Order.GetDeliveryPrice(RestaurantService.CurrentRestaurantId, DishesTotal);
            TotalPrice = DishesTotal + DeliveryPrice;
            IsBusy = false;
        }
    }
}

