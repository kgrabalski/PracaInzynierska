using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantInfoModel
    {
        public RestaurantDetails RestaurantDetails { get; set; }
        public IEnumerable<OpeningHour> OpeningHours { get; set; }
    }
}
