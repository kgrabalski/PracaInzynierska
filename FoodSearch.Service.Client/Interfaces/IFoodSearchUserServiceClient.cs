using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IFoodSearchUserServiceClient 
    {
        Task<bool> Login(string email, string password);
        Task<bool> Logout();
        Task<RegistrationResult> Register(string firstName, string lastName, string email, string phoneNumber, string password, string repeatPassword, int addressId, string flatNumber);
    }
}
