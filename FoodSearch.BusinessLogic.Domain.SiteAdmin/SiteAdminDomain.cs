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
                    MinOrderAmount = 0f
                };
                return rep.Save<Guid>(restaurant);
            }
        }
    }
}
