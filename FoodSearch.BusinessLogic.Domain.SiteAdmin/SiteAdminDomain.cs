using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin
{
    public class SiteAdminDomain : ISiteAdminDomain
    {
        private readonly IRepositoryProvider _provider;

        public SiteAdminDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<Models.Restaurant> GetRestaurants(Guid? restaurantId = null)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                if (restaurantId.HasValue)
                {
                    var r = rep.Get(restaurantId.Value);
                    return new[]
                    {
                        TransformRestaurant(r)
                    };
                }

                return rep.GetAll()
                    .Where(x => x.IsDeleted == false)
                    .List()
                    .Select(TransformRestaurant)
                    .ToList();
            }
        }

        private static Models.Restaurant TransformRestaurant(Restaurant r)
        {
            return new Models.Restaurant()
            {
                RestaurantId = r.RestaurantId,
                Name = r.Name,
                LogoId = r.ImageId,
                City = r.Address.City.Name,
                District = r.Address.District.Name,
                Street = r.Address.Street.Name,
                Number = r.Address.Number
            };
        }

        public Guid CreateRestaurant(string name, int addressId, int logoId)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                Restaurant restaurant = new Restaurant()
                {
                    Name = name,
                    AddressId = addressId,
                    ImageId = logoId,
                    IsOpen = false,
                    IsDeleted = false,
                    MinOrderAmount = 0f
                };
                return rep.Create<Guid>(restaurant);
            }
        }

        public void DeleteRestaurant(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                var r = rep.Get(restaurantId);
                try
                {
                    rep.Delete(r);
                }
                catch (Exception)
                {
                    r.IsDeleted = true;
                    rep.Update(r);
                }
            }
        }
    }
}
