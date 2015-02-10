using System;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;
using Xamarin.Forms;
using FoodSearch.Service.Client.Contracts;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OrderSuccededViewModel : ViewModelBase
    {
        private Guid _orderId = Guid.Empty;

        public OrderSuccededViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            Services.Messaging.Register<Guid>(InitializeView);
        }

        private async void InitializeView(Guid orderId)
        {
            await Client.Order.ClearBasket();
            _orderId = orderId;
            while (IsWaiting)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                var status = await Client.Order.GetDeliveryStatus(_orderId);

                if (status.ConfirmationStatus != (int)ConfirmationStatus.NotConfirmed) {
                    IsWaiting = false;
                    IsConfirmed = status.ConfirmationStatus == (int)ConfirmationStatus.Confirmed;
                    IsCancelled = status.ConfirmationStatus == (int)ConfirmationStatus.Cancelled;
                    DeliveryTime = status.DeliveryDate;
                    CancellationReason = status.CancellationReason;
                }
            }
        }

        private bool _isWaiting = true;

        public bool IsWaiting
        {
            get { return _isWaiting; }
            set { SetProperty(ref _isWaiting, value); }
        }

        private bool _isConfirmed = false;

        public bool IsConfirmed
        {
            get { return _isConfirmed; }
            set { SetProperty(ref _isConfirmed, value); }
        }

        private bool _isCancelled = false;

        public bool IsCancelled
        {
            get { return _isCancelled; }
            set { SetProperty(ref _isCancelled, value); }
        }

        private string _deliveryTime = string.Empty;

        public string DeliveryTime
        {
            get { return _deliveryTime; }
            set { SetProperty(ref _deliveryTime, value); }
        }

        private string _cancellationReason;

        public string CancellationReason
        {
            get { return _cancellationReason; }
            set { SetProperty(ref _cancellationReason, value); }
        }

    }
}

