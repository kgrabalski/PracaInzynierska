using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
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
        private CookieContainer cookieContainer = new CookieContainer();

        protected override string ServiceAddress
        {
            get { return _baseAddress + "Order/"; }
        }

        public async Task<ObservableCollection<BasketItem>> GetBasket()
        {
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
				
                var response = await client.GetAsync(ServiceAddress + "Basket");
                var httpResponse = new HttpBodyResponse<string>()
                {
                    Body = await response.Content.ReadAsStringAsync(), 
                    StatusCode = response.StatusCode
                };

                return DeserializeList<BasketItem>(httpResponse);
            }
        }

        public async Task<bool> AddToBasket(int dishId)
        {
            using (var handler = new HttpClientHandler() {CookieContainer = cookieContainer} )
            using (var client = new HttpClient(handler))
            {
                var response = await client.PostAsync(ServiceAddress + "Basket/" + dishId, new StringContent(string.Empty));
                return response.StatusCode == HttpStatusCode.Created;
            }
        }

        public async Task<bool> RemoveFromBasket(int dishId)
        {
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
                var response = await Delete("Basket/" + dishId);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
        }

        public async Task<bool> ClearBasket()
        {
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
                var response = await Delete("Basket");
				cookieContainer = new CookieContainer();
				return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
        }

        public async Task<decimal> GetDeliveryPrice(Guid restaurantId, decimal totalPrice)
        {
            var request = new GetDeliveryPriceRequest()
            {
                RestaurantId = restaurantId,
                TotalPrice = totalPrice
            };
            string jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(ServiceAddress + "DeliveryPrice", httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
            return decimal.Parse(responseString, CultureInfo.InvariantCulture);
        }
    }
}

