using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Restaurant.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Mapping;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.Data.Mapping.Interface;

using Dish = FoodSearch.Data.Mapping.Entities.Dish;
using DishDto = FoodSearch.BusinessLogic.Domain.Restaurant.Models.Dish;
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

        public IEnumerable<DishGroup> GetDishes(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Dish>())
            {
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .List()
                    .GroupBy(x => new {x.DishGroupId, x.DishGroup.Name})
                    .Select(x => new DishGroup()
                    {
                        Id = x.Key.DishGroupId,
                        Name = x.Key.Name,
                        Dishes = x.Select(d => new DishDto()
                        {
                            Id = d.DishId,
                            Name = d.DishName,
                            Price = d.Price
                        }).ToList()
                    });
            }
        }

        public DishDto GetDish(int dishId)
        {
            using (var rep = _provider.GetRepository<Dish>())
            {
                return rep.Get(dishId).Map<DishDto>();
            }
        }

        public IEnumerable<PartnerRestaurant> GetPartnerRestaurants()
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetPartnerRestaurants().Map<IEnumerable<PartnerRestaurant>>();
            }
        }
    }
}
