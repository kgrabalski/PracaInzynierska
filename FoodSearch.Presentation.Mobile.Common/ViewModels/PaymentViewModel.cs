using System;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        public PaymentViewModel(IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService) : base(client, authorizationService, dialogService)
        {
            MessageService.Register<CreateOrderResult>(InitializeView);
        }

        private void InitializeView(CreateOrderResult order)
        {
            string ipn = "http://foodsearch.azurewebsites.net/Order/UserPaymentIpn";
            string success = "http://foodsearch.azurewebsites.net/Order/Success";
            string cancel = "http://foodsearch.azurewebsites.net/Order/Cancel?paymentId=" + order.PaymentId;
            string url = string.Format("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=kgrabalski@poczta.onet.pl&item_number={0}&item_name={1}&rm=2&LC=PL&country=PL&amount={2}&currency_code=PLN&notify_url={3}&return={4}&cancel_return={5}",
                order.PaymentId,
                "Restaurant " + order.RestaurantName,
                order.Price,
                ipn,
                success,
                cancel);
            PaymentUrl = url;
        }

        private string _paymentUrl = string.Empty;

        public string PaymentUrl
        {
            get { return _paymentUrl; }
            set { SetProperty(ref _paymentUrl, value); }
        }

    }
}

