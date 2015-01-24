using System.Xml.Linq;

using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.StoredProcedure.Results;
using System;
using System.Collections.Generic;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface IStoredProcedureRepository : IDisposable
    {
        IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date, string filterXml);
        int CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo);
        IEnumerable<StreetResult> GetStreets(int cityId, string query);
        IEnumerable<PartnerRestaurant> GetPartnerRestaurants();
        RestaurantRating GetRestaurantRating(Guid restaurantId);
        IEnumerable<UserOrder> GetUserOrders(Guid userId, int page = 0, int pageSize = 10);
        string GetRestaurantOrders(Guid restaurantId, Guid? orderId, OrderStates? ordersState);
        Guid CreateRestaurant(string restaurantName, int addressId, int logoId, string userFirstName, string userLastName, string userEmail, string userPhone, byte[] userPassword);
        RestaurantDeliveryRange GetDeliveryRange(Guid restaurantId);
    }
}
