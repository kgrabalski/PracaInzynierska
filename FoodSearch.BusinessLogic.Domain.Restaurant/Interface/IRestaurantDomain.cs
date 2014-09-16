using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;

namespace FoodSearch.BusinessLogic.Domain.Restaurant.Interface
{
    public interface IRestaurantDomain
    {
        IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date);
    }
}
