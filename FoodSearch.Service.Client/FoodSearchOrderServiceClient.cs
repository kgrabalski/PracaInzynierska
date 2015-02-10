using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using System.Net;
using System.Net.Http;
using FoodSearch.Service.Client.Response;
using FoodSearch.Service.Client.Requests;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;

namespace FoodSearch.Service.Client
{
    public class FoodSearchOrderServiceClient : ServiceClientBase, IFoodSearchOrderServiceClient
    {
        public FoodSearchOrderServiceClient(CookieContainer cookieContainer) : base(cookieContainer)
        {
        }

        protected override string ServiceAddress
        {
            get { return _baseAddress + "Order/"; }
        }

        public async Task<ObservableCollection<BasketItem>> GetBasket()
        {
            var response = await Get("Basket");
            return DeserializeList<BasketItem>(response);
        }

        public async Task<bool> AddToBasket(int dishId)
        {
            var response = await Post("Basket/" + dishId, null);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<bool> RemoveFromBasket(int dishId)
        {
            var response = await Delete("Basket/" + dishId);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> ClearBasket()
        {
            var response = await Delete("Basket");
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<decimal> GetDeliveryPrice(Guid restaurantId, decimal totalPrice)
        {
            var request = new GetDeliveryPriceRequest()
            {
                RestaurantId = restaurantId,
                TotalPrice = totalPrice
            };
            var response = await Post("DeliveryPrice", request);
            return decimal.Parse(response.Body, CultureInfo.InvariantCulture);
        }

        public async Task<DeliveryAddress> GetDeliveryAddress()
        {
            var response = await Get("DeliveryAddress");
            return Deserialize<DeliveryAddress>(response.Body);
        }

        public async Task<CreateOrderResult> CreateOrder(PaymentTypes paymentType, DeliveryTypes deliveryType, int addressId, string flatNumber)
        {
            var request = new CreateOrderRequest()
            {
                PaymentTypeId = (int)paymentType,
                DeliveryTypeId = (int)deliveryType,
                AddressId = addressId,
                FlatNumber = flatNumber
            };
            var response = await Post("Order", request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var result = Deserialize<CreateOrderResult>(response.Body);
                result.Succeed = true;
                return result;
            }
            return new CreateOrderResult() { Succeed = false };
        }

        public async Task SetCurrentRestaurant(Guid restaurantId)
        {
            await Post("CurrentRestaurant/" + restaurantId.ToString(), null);
        }

        public async Task<DeliveryStatus> GetDeliveryStatus(Guid orderId)
        {
            var response = await Get("Order/" + orderId.ToString());
            return Deserialize<DeliveryStatus>(response.Body);
        }
    }
}

