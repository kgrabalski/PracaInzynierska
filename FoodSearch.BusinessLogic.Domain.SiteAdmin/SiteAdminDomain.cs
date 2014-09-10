using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Mapping;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

using Restaurant = FoodSearch.Data.Mapping.Entities.Restaurant;
using RestaurantDto = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Restaurant;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin
{
    public class SiteAdminDomain : ISiteAdminDomain
    {
        private readonly IRepositoryProvider _provider;

        public SiteAdminDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<RestaurantDto> GetRestaurants()
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                return rep.GetAll()
                    .Where(x => x.IsDeleted == false)
                    .OrderBy(x => x.Name).Asc
                    .List()
                    .ToList()
                    .Map<IEnumerable<RestaurantDto>>();
            }
        }

        public RestaurantDto CreateRestaurant(string name, int addressId, int logoId)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                bool canCreate = rep.GetAll()
                    .Where(x => x.Name == name && x.AddressId == addressId)
                    .RowCount() == 0;
                if (!canCreate) return null;

                var newRestaurant = new Restaurant()
                {
                    Name = name,
                    AddressId = addressId,
                    ImageId = logoId,
                    IsDeleted = false,
                    IsOpen = false,
                    MinOrderAmount = 0f
                };
                Guid restaurantId = rep.Create<Guid>(newRestaurant);

                rep.Evict(newRestaurant);
                return rep.Get(restaurantId).Map<RestaurantDto>();
            }
        }

        public bool DeleteRestaurant(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                return rep.TryDelete(restaurantId);
            }
        }
    }
}
