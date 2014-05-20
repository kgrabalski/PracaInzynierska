using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.StoredProcedure.Results;

using NHibernate;

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

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
