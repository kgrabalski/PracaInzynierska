using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using FoodSearch.BusinessLogic.Domain.Restaurant.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Mapping;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.Data.Mapping.Interface;

using Dish = FoodSearch.Data.Mapping.Entities.Dish;
using DishDto = FoodSearch.BusinessLogic.Domain.Restaurant.Models.Dish;
using Opinion = FoodSearch.Data.Mapping.Entities.Opinion;
using OpinionDto = FoodSearch.BusinessLogic.Domain.Restaurant.Models.Opinion;
using RestaurantInfoDto = FoodSearch.BusinessLogic.Domain.Restaurant.Models.RestaurantInfo;
using RestaurantRatingDto = FoodSearch.BusinessLogic.Domain.Restaurant.Models.RestaurantRating;

namespace FoodSearch.BusinessLogic.Domain.Restaurant
{
    public class RestaurantDomain : IRestaurantDomain
    {
        private readonly IRepositoryProvider _provider;

        public RestaurantDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<RestaurantInfoDto> GetRestaurants(int addressId, DateTime date, RestaurantFilter filter = null)
        {
            using (var rep = _provider.StoredProcedure)
            {
                string filterXml = null;
                if (filter != null)
                {
                    filterXml = new XElement("filter",
                        new object[]
                        {
                            new XElement("cuisines", filter.Cuisines.Select(x => new XElement("cuisine", new XAttribute("id", x))))
                        })
                        .ToString();
                }
                return rep.GetRestaurants(addressId, date, filterXml).Map<IEnumerable<RestaurantInfoDto>>();
            }
        }

        public string GetRestaurantName(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Restaurant>())
            {
                Data.Mapping.Entities.Restaurant r;
                return rep.TryGet(restaurantId, out r) ? r.Name : null;
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
                            Price = d.Price,
                            ImageId = d.ImageId
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

        public bool AddOpinion(Guid restaurantId, Guid userId, int rating, string comment)
        {
            using (var rep = _provider.GetRepository<Opinion>())
            {
                bool canCreate = rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId && x.UserId == userId)
                    .RowCount() == 0;
                if (canCreate)
                {
                    return rep.Create<int>(new Opinion()
                    {
                        RestaurantId = restaurantId,
                        UserId = userId,
                        Rating = (short)rating,
                        Comment = comment,
                        CreateDate = DateTime.Now
                    }) > 0;
                }
                return false;
            }
        }

        public IEnumerable<OpinionDto> GetOpinions(Guid restaurantId, int rating = 0, int page = 0)
        {
            using (var rep = _provider.GetRepository<Opinion>())
            {
                int pageSize = 10;
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId &&
                        (rating == 0 || x.Rating == rating))
                    .OrderBy(x => x.CreateDate).Desc
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

        public RestaurantRatingDto GetRestaurantRating(Guid restaurantId)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetRestaurantRating(restaurantId).Map<RestaurantRatingDto>();
            }
        }

        public bool CheckUserCommentExists(Guid userId, Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Opinion>())
            {
                return rep.GetAll()
                    .Where(x => x.UserId == userId && x.RestaurantId == restaurantId)
                    .RowCount() > 0;
            }
        }
    }
}
