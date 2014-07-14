using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.StoredProcedure.Results;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface IStoredProcedureRepository : IDisposable
    {
        IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date);
        int CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo);
    }
}
