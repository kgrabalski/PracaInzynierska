using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FoodSearch.BusinessLogic.Domain.User.Models;

namespace FoodSearch.BusinessLogic.Domain.User.Interface
{
    public interface IUserDomain
    {
        bool IsUserNameDuplicated(string userName);
        bool IsEmailDuplicated(string email);
        Guid CreateUser(string userName, string firstName, string lastName, string email, byte[] password);
        void CreateConfirmationEntry(Guid userId, string email, string userName);
        RegisterConfirmationResult ConfirmRegistration(Guid code);
        bool ValidateUser(string userName, byte[] password);
        string GetUserRole(string userName);
        bool ValidateUserRole(string userName, string role);
    }
}
