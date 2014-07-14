using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Data.Mapping.Entities;

using Dish = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.Dish;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface
{
    public interface IRestaurantAdminDomain
    {
        Guid CreateUser(Guid restaurantId, string userName, string firstName, string lastName, string email, string password, UserTypes userType);
        bool ChangeRestaurantState(Guid restaurantId, bool isOpened);
        IEnumerable<Cuisine> GetCuisines();
        IEnumerable<Cuisine> GetRestaurantCuisines(Guid restaurantId);
        bool AddRestaurantCuisine(Guid restaurantId, int cuisineId);
        bool RemoveRestaurantCuisine(Guid restaurantId, int cuisineId);
        IEnumerable<DishGroup> GetDishGroups(Guid restaurantId);
        int CreateDishGroup(Guid restaurantId, string groupName);
        void EditDishGroup(int dishGroupId, string newGroupName);
        void DeleteDishGroup(int dishGroupId);
        IEnumerable<Dish> GetDishes(Guid restaurantId, int? dishId = null);
        int CreateDish(Guid restaurantId, string dishName, int dishGroupId, float price);
        Guid GetUserId(string userName);
        Guid GetUserRestaurant(Guid userId);
        IEnumerable<Models.OpeningHour> GetOpeningHours(Guid restaurantId, int? openingHourId = null);
        int CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo);
        void DeleteOpeningHour(int openingHourId);
        EmployeeData GetEmployeeData(Guid restaurantId, Guid userId);
    }
}
