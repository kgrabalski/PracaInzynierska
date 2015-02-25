using System;
using System.Collections.Generic;

using FoodSearch.BusinessLogic.Domain.SiteAdmin.Models;
using FoodSearch.Data.Mapping.Entities;

using Address = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Address;
using User = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.User;
using Restaurant = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Restaurant;


namespace FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface
{
    public interface ISiteAdminDomain
    {
        IEnumerable<Restaurant> GetRestaurants(Guid? restaurantId = null);
        Guid? CreateRestaurant(string name, int addressId, int logoId, string userPassword, string userEmail, string userFirstName, string userLastName);
        bool DeleteRestaurant(Guid restaurantId);
        Address GetAddress(int addressId);
        IEnumerable<User> GetUsers(string query, int? page, int? pageSize);
        bool ChangeUserState(Guid userId, UserStates userState);
        IEnumerable<SystemDailyFinancialReport> GetSystemDailyFinancialReport(DateTime dateFrom, DateTime dateTo);
        IEnumerable<SystemMonthlyFinancialReport> GetSystemMonthlyFinancialReport(DateTime dateFrom, DateTime dateTo);
    }
}
