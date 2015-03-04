using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        private string _pageSuccess = "http://foodsearch.azurewebsites.net/OrderMobile/Success?paymentId=";
        private string _pageCancel = "http://foodsearch.azurewebsites.net/OrderMobile/Cancel?paymentId=";
        private Guid _orderId = Guid.Empty;

        public PaymentViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            Services.Messaging.Register<CreateOrderResult>(InitializeView);
        }

        private async void InitializeView(CreateOrderResult order)
        {
            string ipn = "http://foodsearch.azurewebsites.net/PayPal/Ipn";
            _pageCancel += order.PaymentId;
            _pageSuccess += order.PaymentId;
            _orderId = order.OrderId;
            
            string url = string.Format("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=kgrabalski@poczta.onet.pl&item_number={0}&item_name={1}&rm=2&LC=PL&country=PL&amount={2}&currency_code=PLN&notify_url={3}&return={4}&cancel_return={5}",
                order.PaymentId,
                "Restaurant " + order.RestaurantName,
                order.Price.ToString("0.00", CultureInfo.InvariantCulture),
                ipn,
                _pageSuccess,
                _pageCancel);
            PaymentUrl = url;
            Services.Dialog.ShowLoading("Ładowanie PayPal");
            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                Services.Dialog.HideLoading();
                return false;
            });

            //signalr
            var hubConnection = new HubConnection("http://foodsearch.azurewebsites.net");
            var hubProxy = hubConnection.CreateHubProxy("FoodSearchMobile");
            hubProxy.On<bool>("UpdatePaymentStatus", UpdatePaymentStatus);
            await hubConnection.Start();
            await hubProxy.Invoke("Register", order.PaymentId);
        }

        private string _paymentUrl = "http://foodsearch.azurewebsites.net/home/blank";

        public string PaymentUrl
        {
            get { return _paymentUrl; }
            set { SetProperty(ref _paymentUrl, value); }
        }

        public IFoodSearchServiceClient ServiceClient { get { return Client; } }

        public void UpdatePaymentStatus(bool status)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await Services.Navigation.Navigate.PopToRootAsync();
                if (status)
                {
                    await Services.Navigation.Navigate.PushAsync(ViewLocator.OrderSucceded);
                    Services.Messaging.Send(_orderId);
                }
                else await Services.Navigation.Navigate.PushAsync(ViewLocator.OrderFailed);
            });
        }
    }
}

