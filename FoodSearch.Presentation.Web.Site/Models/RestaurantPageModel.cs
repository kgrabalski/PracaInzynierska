using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;

using DishGroup = FoodSearch.BusinessLogic.Domain.Restaurant.Models.DishGroup;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantPageModel
    {
        public RestaurantDetails RestaurantDetails { get; set; }
        public IEnumerable<DishGroup> DishGroups { get; set; }
        public IEnumerable<Opinion> Opinions { get; set; }
        public IEnumerable<OpeningHour> OpeningHours { get; set; }
        public RestaurantRating RestaurantRating { get; set; }
    }
}