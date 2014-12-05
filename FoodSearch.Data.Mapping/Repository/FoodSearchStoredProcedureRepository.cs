﻿using FoodSearch.Data.Mapping.Interface;
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

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
