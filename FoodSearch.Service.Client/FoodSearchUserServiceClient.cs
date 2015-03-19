using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client.Requests;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client
{
    public class FoodSearchUserServiceClient : ServiceClientBase, IFoodSearchUserServiceClient
    {
        public FoodSearchUserServiceClient(CookieContainer cookieContainer) : base(cookieContainer)
        {
        }

        protected override string ServiceAddress
        {
            get { return BaseAddress + "User/"; }
        }

        public async Task<bool> Login(string email, string password)
        {
            var request = new LoginRequest()
            {
                Email = email,
                Password = password
            };
            var response = await Post("User", request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> Logout()
        {
            var response = await Delete("User");
            return response.StatusCode == HttpStatusCode.OK; 
        }

        public async Task<RegistrationResult> Register(string firstName, string lastName, string email, string phoneNumber, string password, string repeatPassword, int addressId, string flatNumber)
        {
            var request = new RegisterRequest()
            {
                FirstName = firstName,
                LastName = lastName, 
                Email = email,
                PhoneNumber = phoneNumber,
                Password = password,
                RepeatPassword = repeatPassword,
                AddressId = addressId,
                FlatNumber = flatNumber
            };
            var response = await Put("User", request);
            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RegistrationResult.Created;
                case HttpStatusCode.Conflict:
                    return RegistrationResult.EmailDuplicated;
                default:
                    return RegistrationResult.NotCreated;
            }
        }

        public async Task<UserDetails> GetUserDetails()
        {
            var response = await Get("UserData");
            return Deserialize<UserDetails>(response.Body);
        }

        public async Task<ObservableCollection<UserOrder>> GetUserOrders(int page, int pageSize = 10)
        {
            string url = string.Format("UserOrder?page={0}&pageSize={1}", page, pageSize);
            var response = await Get(url);
            return DeserializeList<UserOrder>(response);
        }

        public async Task<ObservableCollection<UserOrderItem>> GetUserOrderItems(Guid orderId)
        {
            var response = await Get("UserOrder/" + orderId.ToString());
            return DeserializeList<UserOrderItem>(response);
        }
    }
}
