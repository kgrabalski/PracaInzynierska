using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantsListModel
    {
        public int AddressId { get; set; }
        public IEnumerable<RestaurantInfo> Restaurants { get; set; }
        public IEnumerable<Cuisine> Cuisines { get; set; }
    }
}
