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
            Services.Dialog.HideLoading();
        }

        private ObservableCollection<UserOrderItem> _orderItems = new ObservableCollection<UserOrderItem>();

        public ObservableCollection<UserOrderItem> OrderItems
        {
            get { return _orderItems; }
            set { SetProperty(ref _orderItems, value); }
        }


    }
}

