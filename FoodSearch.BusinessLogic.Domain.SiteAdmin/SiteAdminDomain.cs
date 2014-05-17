using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin
{
    public class SiteAdminDomain : ISiteAdminDomain
    {
        private readonly IRepositoryProvider _provider;

        public SiteAdminDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public Models.Restaurant GetRestaurant(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                var r = rep.Get(restaurantId);
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
        }

        public IEnumerable<Models.Restaurant> GetRestaurants()
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                return rep.GetAll()
                    .Where(x => x.IsDeleted == false)
                    .List()
                    .Select(x => new Models.Restaurant()
                    {
                        RestaurantId = x.RestaurantId,
                        Name = x.Name,
                        LogoId = x.ImageId,
                        City = x.Address.City.Name,
                        District = x.Address.District.Name,
                        Street = x.Address.Street.Name,
                        Number = x.Address.Number
                    })
                    .ToList();
            }
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
