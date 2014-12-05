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
    }
}
