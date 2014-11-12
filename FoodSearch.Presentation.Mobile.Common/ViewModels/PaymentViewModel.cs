using System;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;
using System.Globalization;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        private readonly string _pageSuccess = "http://foodsearch.azurewebsites.net/Order/Success";
        private string _pageCancel = "http://foodsearch.azurewebsites.net/Order/Cancel?paymentId=";

        public PaymentViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            Services.Messaging.Register<CreateOrderResult>(InitializeView);
        }

        private void InitializeView(CreateOrderResult order)
        {
            string ipn = "http://foodsearch.azurewebsites.net/Order/UserPaymentIpn";
            _pageCancel += order.PaymentId;
            
            string url = string.Format("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=kgrabalski@poczta.onet.pl&item_number={0}&item_name={1}&rm=2&LC=PL&country=PL&amount={2}&currency_code=PLN&notify_url={3}&return={4}&cancel_return={5}",
                order.PaymentId,
                "Restaurant " + order.RestaurantName,
                order.Price.ToString("0.00", CultureInfo.InvariantCulture),
                ipn,
                _pageSuccess,
                _pageCancel);
            PaymentUrl = url;
        }

        private string _paymentUrl = "http://foodsearch.azurewebsites.net/home/blank";

        public string PaymentUrl
        {
            get { return _paymentUrl; }
            set { SetProperty(ref _paymentUrl, value); }
        }

        public IFoodSearchServiceClient ServiceClient { get { return Client; } }

        public async void OnUrlChanged(string url)
        {
            if (url == _pageSuccess)
            {
                await Services.Navigation.Navigate.PopToRootAsync();
                await Services.Navigation.Navigate.PushAsync(ViewLocator.OrderSucceded);
                return;
            }

            if (url == _pageCancel)
            {
                await Services.Navigation.Navigate.PopToRootAsync();
                await Services.Navigation.Navigate.PushAsync(ViewLocator.OrderFailed);
            }
        }
    }
}

