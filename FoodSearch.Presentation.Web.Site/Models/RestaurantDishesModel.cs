using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantDishesModel
    {
        public IEnumerable<DishGroup> DishGroups { get; set; }
        public Basket Basket { get; set; }
    }
}