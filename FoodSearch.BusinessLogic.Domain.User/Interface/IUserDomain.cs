using System.Collections.Generic;

using FoodSearch.BusinessLogic.Domain.User.Models;
using System;

namespace FoodSearch.BusinessLogic.Domain.User.Interface
{
    public interface IUserDomain
    {
        bool IsEmailDuplicated(string email);
        Guid CreateUser(string firstName, string lastName, string email, string phoneNumber, string password);
        void CreateConfirmationEntry(Guid userId, string email);
        RegisterConfirmationResult ConfirmRegistration(Guid code);
        bool ValidateUser(string email, byte[] password);
        string GetUserRole(string email);
        bool ValidateUserRole(string email, string role);
        int CreateDeliveryAddress(Guid userId, int addressId, string flatNumber);
        UserDetails GetUserDetails(Guid userId);
        Guid GetUserId(string user);
        Guid CreatePasswordResetRequest(string email);
        IEnumerable<UserOrderItem> GetUserOrderItems(Guid orderId);
        IEnumerable<UserOrder> GetUserOrders(Guid userId, int page = 0, int pageSize = 10);
        bool ChangeUserPassword(Guid userId, string oldPassword, string newPassword);
    }
}
