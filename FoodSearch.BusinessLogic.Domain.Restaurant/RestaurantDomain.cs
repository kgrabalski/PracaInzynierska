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
using Opinion = FoodSearch.Data.Mapping.Entities.Opinion;
using OpinionDto = FoodSearch.BusinessLogic.Domain.Restaurant.Models.Opinion;
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

        public decimal GetDeliveryPrice(Guid restaurantId, decimal totalPrice)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Restaurant>())
            {
                var rest = rep.Get(restaurantId);
                decimal freeDelivery = (decimal)rest.FreeDeliveryFrom;
                if (totalPrice >= freeDelivery) return decimal.Zero;
                return (decimal) rest.DeliveryPrice;
            }
        }

        public IEnumerable<OpinionDto> GetOpinions(Guid restaurantId, int page = 0, int pageSize = 10)
        {
            using (var rep = _provider.GetRepository<Opinion>())
            {
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .Skip(page*pageSize)
                    .Take(pageSize)
                    .List()
                    .Map<IEnumerable<OpinionDto>>();
            }
        }

        public RestaurantDetails GetRestaurantDetails(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Restaurant>())
            {
                var r = rep.Get(restaurantId);
                return new RestaurantDetails()
                {
                    RestaurantId = r.RestaurantId,
                    RestaurantName = r.Name,
                    LogoId = r.ImageId,
                    AddressId = r.AddressId,
                    City = r.Address.District.City.Name,
                    District = r.Address.District.Name,
                    Street = r.Address.Street.Name,
                    Number = r.Address.Number,
                    Lat = r.Address.Lat,
                    Lon = r.Address.Lon,
                    MinimumOrder = r.MinOrderAmount,
                    DeliveryPrice = r.DeliveryPrice,
                    FreeDeliveryFrom = r.FreeDeliveryFrom
                };
            }
        }

        public RestaurantRating GetRestaurantRating(Guid restaurantId)
        {
            return new RestaurantRating();
        }
    }
}
