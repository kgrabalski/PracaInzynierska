using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using System.Collections.Generic;
using DishGroup = FoodSearch.BusinessLogic.Domain.Restaurant.Models.DishGroup;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantPageModel
    {
        public RestaurantDetails RestaurantDetails { get; set; }
        public IEnumerable<DishGroup> DishGroups { get; set; }
        public IEnumerable<OpeningHour> OpeningHours { get; set; }
        public bool UserCommented { get; set; }
    }
}