using System;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Contracts;
using System.Collections.ObjectModel;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OrderDetailsViewModel : ViewModelBase
    {
        public OrderDetailsViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            Services.Messaging.Register<UserOrder>(GetOrderDetails);
        }

        private async void GetOrderDetails(UserOrder order)
        {
            Services.Dialog.ShowLoading("Wczytywanie zamówienia...");
            OrderItems = await Client.User.GetUserOrderItems(order.OrderId);
            DeliveryType = order.DeliveryType;
            DeliveryAddress = order.DeliveryAddress;
            OrderAmount = order.OrderAmount;
            RestaurantName = order.RestaurantName;
            CreateDate = order.CreateDate;
            Services.Dialog.HideLoading();
        }

        private ObservableCollection<UserOrderItem> _orderItems = new ObservableCollection<UserOrderItem>();

        public ObservableCollection<UserOrderItem> OrderItems
        {
            get { return _orderItems; }
            set { SetProperty(ref _orderItems, value); }
        }

        private string _deliveryType = string.Empty;

        public string DeliveryType
        {
            get { return _deliveryType; }
            set { SetProperty(ref _deliveryType, value); }
        }

        private string _deliveryAddress = string.Empty;

        public string DeliveryAddress
        {
            get { return _deliveryAddress; }
            set { SetProperty(ref _deliveryAddress, value); }
        }

        private string _orderAmount = "0.00 zł";

        public string OrderAmount
        {
            get { return _orderAmount; }
            set { SetProperty(ref _orderAmount, value); }
        }

        private string _restaurantName;

        public string RestaurantName
        {
            get { return _restaurantName; }
            set { SetProperty(ref _restaurantName, value); }
        }

        private string _createDate;

        public string CreateDate
        {
            get { return _createDate; }
            set { SetProperty(ref _createDate, value); }
        }

    }
}

