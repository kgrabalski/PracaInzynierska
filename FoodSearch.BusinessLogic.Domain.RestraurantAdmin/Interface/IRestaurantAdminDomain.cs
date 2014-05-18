using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.Entities;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface
{
    public interface IRestaurantAdminDomain
    {
        Guid CreateUser(Guid restaurantId, string userName, string firstName, string lastName, string email, string password, UserTypes userType);
        bool ChangeRestaurantState(Guid restaurantId, bool isOpened);
        int CreateCuisine(string name);
        void AddRestaurantCuisine(Guid restaurantId, int cuisineId);
        bool RemoveRestaurantCuisine(Guid restaurantId, int cuisineId);
        int CreateDishGroup(Guid restaurantId, string groupName);
        void EditDishGroup(int dishGroupId, string newGroupName);
        int CreateDish(Guid restaurantId, string dishName, int dishGroupId, float price);
        Guid GetUserId(string userName);
        Guid GetUserRestaurant(Guid userId);
        IEnumerable<Models.OpeningHour> GetOpeningHours(Guid restaurantId, int? openingHourId = null);
        int CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo);
        void DeleteOpeningHour(int openingHourId);
    }
}
