using System;
using System.Collections.Generic;

using FoodSearch.Data.Mapping.Entities;

using Address = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Address;
using City = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.City;
using Restaurant = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Restaurant;


namespace FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface
{
    public interface ISiteAdminDomain
    {
        IEnumerable<Restaurant> GetRestaurants(Guid? restaurantId = null);
        Guid? CreateRestaurant(string name, int addressId, int logoId, string userPassword, string userEmail, string userFirstName, string userLastName);
        bool DeleteRestaurant(Guid restaurantId);
        Address GetAddress(int addressId);
        Guid CreateUser(string email, string userPassword, string firstName, string lastName, UserTypes userType, UserStates userState);
    }
}
