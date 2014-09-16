using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Restaurant.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Mapping;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.Data.Mapping.Interface;

using RestaurantInfoDto = FoodSearch.BusinessLogic.Domain.Restaurant.Models.RestaurantInfo;

namespace FoodSearch.BusinessLogic.Domain.Restaurant
{
    public class RestaurantDomain : IRestaurantDomain
    {
        private readonly IRepositoryProvider _provider;

        public RestaurantDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<RestaurantInfoDto> GetRestaurants(int addressId, DateTime date)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetRestaurants(addressId, date).Map<IEnumerable<RestaurantInfoDto>>();
            }
        }
    }
}
