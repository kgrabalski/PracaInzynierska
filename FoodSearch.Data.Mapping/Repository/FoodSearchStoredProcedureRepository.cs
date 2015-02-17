using System.Xml.Linq;

using FoodSearch.Data.Mapping.Entities;
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

        public IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date, string filterXml)
        {
            var query = _session.GetNamedQuery("GetRestaurants");
            query.SetInt32("addressId", addressId);
            query.SetDateTime("date", date);
            if (string.IsNullOrEmpty(filterXml)) query.SetParameter("filter", null, NHibernateUtil.String);
            else query.SetString("filter", filterXml);
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

        public IEnumerable<StreetResult> GetStreets(int cityId, string query)
        {
            if (string.IsNullOrEmpty(query)) query = "";
            var q = _session.GetNamedQuery("GetStreets");
            q.SetInt32("cityId", cityId);
            q.SetString("query", query);
            return q.List<StreetResult>();
        }

        public IEnumerable<PartnerRestaurant> GetPartnerRestaurants()
        {
            var query = _session.GetNamedQuery("GetPartnerRestaurants");
            return query.List<PartnerRestaurant>();
        }

        public RestaurantRating GetRestaurantRating(Guid restaurantId)
        {
            var query = _session.GetNamedQuery("GetRestaurantRating");
            query.SetGuid("restaurantId", restaurantId);
            return query.UniqueResult<RestaurantRating>();
        }

        public IEnumerable<UserOrder> GetUserOrders(Guid userId, int page = 0, int pageSize = 10)
        {
            var query = _session.GetNamedQuery("GetUserOrders");
            query.SetGuid("userId", userId);
            query.SetInt32("page", page);
            query.SetInt32("pageSize", pageSize);
            return query.List<UserOrder>();
        }

        public string GetRestaurantOrders(Guid restaurantId, Guid? orderId, OrderStates? ordersState)
        {
            var query = _session.GetNamedQuery("GetRestaurantOrders");
            query.SetGuid("restaurantId", restaurantId);

            if (orderId.HasValue) query.SetGuid("orderId", orderId.Value);
            else query.SetParameter("orderId", null, NHibernateUtil.Guid);

            if (ordersState.HasValue) query.SetInt32("ordersState", (int) ordersState);
            else query.SetParameter("ordersState", null, NHibernateUtil.Int32);

            return query.UniqueResult<string>();
        }

        public Guid CreateRestaurant(string restaurantName, int addressId, int logoId, string userFirstName, string userLastName, string userEmail, string userPhone, byte[] userPassword)
        {
            var query = _session.GetNamedQuery("CreateRestaurant");
            query.SetString("restaurantName", restaurantName);
            query.SetInt32("addressId", addressId);
            query.SetInt32("logoId", logoId);
            query.SetString("userFirstName", userFirstName);
            query.SetString("userLastName", userLastName);
            query.SetString("userEmail", userEmail);
            query.SetString("userPhone", userPhone);
            query.SetBinary("userPassword", userPassword);

            return query.UniqueResult<Guid>();
        }

        public RestaurantDeliveryRange GetDeliveryRange(Guid restaurantId)
        {
            var query = _session.GetNamedQuery("GetDeliveryRange");
            query.SetGuid("restaurantId", restaurantId);

            return query.UniqueResult<RestaurantDeliveryRange>();
        }

        public bool UpdateDeliveryRange(Guid restaurantId, bool hasDeliveryRadius, decimal deliveryRadius, string polygonGml)
        {
            var query = _session.GetNamedQuery("UpdateDeliveryRange");
            query.SetGuid("restaurantId", restaurantId);
            query.SetBoolean("hasDeliveryRadius", hasDeliveryRadius);
            query.SetDecimal("deliveryRadius", deliveryRadius);
            query.SetString("polygonGml", polygonGml);

            return query.UniqueResult<bool>();
        }

        public void ClearImagesTable()
        {
            var query = _session.GetNamedQuery("ClearImagesTable");
            query.ExecuteUpdate();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
