using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.SiteAdmin.Models;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface
{
    public interface ISiteAdminDomain
    {
        IEnumerable<Restaurant> GetRestaurants(Guid? restaurantId = null);
        Guid CreateRestaurant(string name, int addressId, int logoId);
        void DeleteRestaurant(Guid restaurantId);
    }
}
