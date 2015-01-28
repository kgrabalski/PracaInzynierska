using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Data.Mapping.Entities;
using System;
using System.Collections.Generic;
using Cuisine = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.Cuisine;
using Dish = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.Dish;
using DishGroup = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.DishGroup;
using OpeningHour = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.OpeningHour;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface
{
    public interface IRestaurantAdminDomain
    {
        IEnumerable<Cuisine> GetCuisines();
        IEnumerable<Cuisine> GetRestaurantCuisines(Guid restaurantId);
        Cuisine AddRestaurantCuisine(Guid restaurantId, int cuisineId);
        bool RemoveRestaurantCuisine(Guid restaurantId, int cuisineId);
        Guid CreateUser(Guid restaurantId, string firstName, string lastName, string email, string password, UserTypes userType);
        bool ChangeRestaurantState(Guid restaurantId, bool isOpened);
        IEnumerable<DishGroup> GetDishGroups(Guid restaurantId);
        DishGroup CreateDishGroup(Guid restaurantId, string groupName);
        bool EditDishGroup(Guid restaurantId, int dishGroupId, string newGroupName);
        bool DeleteDishGroup(Guid restaurantId, int dishGroupId);
        IEnumerable<Dish> GetDishes(Guid restaurantId);
        Dish CreateDish(Guid restaurantId, string dishName, int dishGroupId, decimal price);
        Guid GetUserId(string email);
        Guid GetUserRestaurant(Guid userId);
        IEnumerable<OpeningHour> GetOpeningHours(Guid restaurantId);
        OpeningHour CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo);
        bool DeleteOpeningHour(int openingHourId);
        EmployeeData GetEmployeeData(Guid restaurantId, Guid userId);
        IEnumerable<RestaurantOrder> GetRestaurantOrders(Guid restaurantId, bool newOrders);
        RestaurantOrder GetRestaurantOrder(Guid restaurantId, Guid orderId);
        RestaurantData GetRestaurantData(Guid restaurantId);
        void UpdateRestaurantData(Guid restaurantId, string restaurantName, bool isOpen, decimal minOrderAmount, decimal deliveryPrice, decimal freeDeliveryFrom);
        DeliveryRange GetDeliveryRange(Guid restaurantId);
        bool UpdateDeliveryRange(Guid restaurantId, bool hasDeliveryRadius, decimal deliveryRadius, IEnumerable<DeliveryRangePoint> polygon);
        IEnumerable<RestaurantEmployee> GetRestaurantEmployees(Guid restaurantId);
        RestaurantEmployee AddRestaurantEmployee(Guid restaurantId, string firstName, string lastName, string password);
        bool DeleteRestaurantEmployee(Guid restaurantId, Guid employeeId);
        void ResetEmployeePassword(Guid userId, string newPassword);
    }
}
