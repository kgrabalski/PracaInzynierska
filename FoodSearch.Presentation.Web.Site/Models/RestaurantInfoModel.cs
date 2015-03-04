using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using System.Collections.Generic;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantInfoModel
    {
        public RestaurantDetails RestaurantDetails { get; set; }
        public IEnumerable<OpeningHour> OpeningHours { get; set; }
    }
}
