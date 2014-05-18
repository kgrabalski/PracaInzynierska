using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantUser
    {
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }
    }
}