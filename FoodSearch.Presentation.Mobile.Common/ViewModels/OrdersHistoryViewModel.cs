using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using System.Collections.ObjectModel;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OrdersHistoryViewModel : ViewModelBase
    {
        public OrdersHistoryViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            InitializeView();
        }

        private async void InitializeView()
        {
            Services.Dialog.ShowLoading("Wczytywanie...");

            Orders = await Client.User.GetUserOrders(_ordersPage);

            Services.Dialog.HideLoading();
        }

        private int _ordersPage = 0;

        private ObservableCollection<UserOrder> _orders;

        public ObservableCollection<UserOrder> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value); }
        }

        private UserOrder _selectedOrder;

        public UserOrder SelectedOrder
        {
            get { return _selectedOrder; }
            set
            { 
                if (SetProperty(ref _selectedOrder, value) && value != null)
                {
                    Services.Navigation.Navigate.PushAsync(ViewLocator.OrderDetails);
                    Services.Messaging.Send(SelectedOrder);
                    SelectedOrder = null;
                }
            }
        }

        private Command _getMoreOrders;

        public Command GetMoreOrders
        {
            get
            {
                return _getMoreOrders ?? (_getMoreOrders = new Command(async () =>
                    {
                        Services.Dialog.ShowLoading("Wczytywanie...");
                        var orders = await Client.User.GetUserOrders(++_ordersPage);
                        foreach (var o in orders)
                        {
                            Orders.Add(o);
                        }
                        Services.Dialog.HideLoading();
                    }));
            }
        }
    }
}

