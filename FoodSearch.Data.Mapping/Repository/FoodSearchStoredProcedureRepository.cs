using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.StoredProcedure.Results;
using NHibernate;
using System;
using System.Collections.Generic;

namespace FoodSearch.Data.Mapping.Repository
{
    public class FoodSearchStoredProcedureRepository : IStoredProcedureRepository
    {
        private readonly ISession _session;

        public FoodSearchStoredProcedureRepository(ISessionSource sessionSource)
        {
            _session = sessionSource.Session;
        }

        public IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date)
        {
            var query = _session.GetNamedQuery("GetRestaurants");
            query.SetInt32("addressId", addressId);
            query.SetDateTime("date", date);
            return query.List<RestaurantInfo>();
        }

        public int CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo)
        {
            var query = _session.GetNamedQuery("CreateOpeningHour");
            query.SetGuid("restaurantId", restaurantId);
            query.SetInt32("day", day);
            query.SetTimeAsTimeSpan("timeFrom", timeFrom);
            query.SetTimeAsTimeSpan("timeTo", timeTo);
            return query.UniqueResult<int>();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
