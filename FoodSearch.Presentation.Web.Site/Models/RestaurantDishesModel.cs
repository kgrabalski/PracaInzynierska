using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantModel : RestaurantPageModel
    {
        public Basket Basket { get; set; }
    }
}