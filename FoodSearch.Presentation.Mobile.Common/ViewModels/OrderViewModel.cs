using System;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;
using System.Collections.ObjectModel;
using FoodSearch.Service.Client.Contracts;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services;
using System.Linq;
using FoodSearch.Presentation.Mobile.Common.Helpers;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        public OrderViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {   
            InitializeView();
        }

        private async void InitializeView()
        {
            if (!Services.Authorization.IsAuthorized)
            {
                await Services.Navigation.Navigate.PopAsync();
                return;
            }

            Services.Dialog.ShowLoading("Ładowanie zamówienia");
            DeliveryTypes = new ObservableCollection<DeliveryType>()
            {
                new DeliveryType() { Id = 1, Name = "Dowóz" },
                new DeliveryType() { Id = 2, Name = "Odbiór osobisty" }
            };
            PaymentTypes = new ObservableCollection<PaymentType>()
            {
                new PaymentType() { Id = 1, Name = "PayPal" },
                new PaymentType() { Id = 2, Name = "Gotówka" }
            };
            var da = await Client.Order.GetDeliveryAddress();
            var basket = await Client.Order.GetBasket();
            decimal dishesTotal = basket.Sum(x => x.Total);
            decimal deliveryPrice = await Client.Order.GetDeliveryPrice(Services.Restaurant.CurrentRestaurantId, dishesTotal);
            decimal total = dishesTotal + deliveryPrice;
            TotalValue = total.ToPln();

            FirstName = da.FirstName;
            LastName = da.LastName;
            City = da.City;
            Street = da.Street;
            StreetNumber = da.StreetNumber;
            FlatNumber = da.FlatNumber;

            Services.Dialog.HideLoading();
        }

        private string _totalValue;

        public string TotalValue
        {
            get { return _totalValue; }
            set { SetProperty(ref _totalValue, value); }
        }

        private ObservableCollection<PaymentType> _paymentTypes = new ObservableCollection<PaymentType>();

        public ObservableCollection<PaymentType> PaymentTypes
        {
            get { return _paymentTypes; }
            set { SetProperty(ref _paymentTypes, value); }
        }

        private PaymentType _selectedPaymentType = new PaymentType();

        public PaymentType SelectedPaymentType
        {
            get { return _selectedPaymentType; }
            set { SetProperty(ref _selectedPaymentType, value); }
        }

        private ObservableCollection<DeliveryType> _deliveryTypes = new ObservableCollection<DeliveryType>();

        public ObservableCollection<DeliveryType> DeliveryTypes
        {
            get { return _deliveryTypes; }
            set { SetProperty(ref _deliveryTypes, value); }
        }

        private DeliveryType _selectedDeliveryType = new DeliveryType();

        public DeliveryType SelectedDeliveryType
        {
            get { return _selectedDeliveryType; }
            set { SetProperty(ref _selectedDeliveryType, value); }
        }

        private string _firstName = string.Empty;

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName = string.Empty;

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _city = string.Empty;

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private string _street = string.Empty;

        public string Street
        {
            get { return _street; }
            set { SetProperty(ref _street, value); }
        }

        private string _strretNumber = string.Empty;

        public string StreetNumber
        {
            get { return _strretNumber; }
            set { SetProperty(ref _strretNumber, value); }
        }

        private string _flatNumber = string.Empty;

        public string FlatNumber
        {
            get { return _flatNumber; }
            set { SetProperty(ref _flatNumber, value); }
        }

        private Command _orderCommand;

        public Command OrderCommand
        {
            get
            {
                return _orderCommand ?? (_orderCommand = new Command(async () =>
                    {
                        if (SelectedDeliveryType.Id == 0 || SelectedPaymentType.Id == 0)
                        {
                            Services.Dialog.Toast("Proszę wybrać sposób płatności i dostawy");
                            return;
                        }

                        Services.Dialog.ShowLoading("Składanie zamówienia");
                        PaymentTypes paymentType = (PaymentTypes)SelectedPaymentType.Id;
                        DeliveryTypes deliveryType = (DeliveryTypes)SelectedDeliveryType.Id;
                        var result = await Client.Order.CreateOrder(paymentType, deliveryType);
                        Services.Dialog.HideLoading();
                        if (result.Succeed)
                        {
                            await Services.Navigation.Navigate.PushModalAsync(ViewLocator.Payment);
                            Services.Messaging.Send(result);
                        } else {
                            await Services.Navigation.Navigate.PopToRootAsync();
                            await Services.Navigation.Navigate.PushAsync(ViewLocator.OrderFailed);
                        }
                    }));
            }
        }
    }
}

