using FoodSearch.Service.Client.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IFoodSearchUserServiceClient 
    {
        Task<bool> Login(string email, string password);
        Task<bool> Logout();
        Task<RegistrationResult> Register(string firstName, string lastName, string email, string phoneNumber, string password, string repeatPassword, int addressId, string flatNumber);
        Task<UserDetails> GetUserDetails();
        Task<ObservableCollection<UserOrder>> GetUserOrders(int page, int pageSize = 10);
        Task<ObservableCollection<UserOrderItem>> GetUserOrderItems(Guid orderId);
    }
}
