using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class DishGroupModel
    {
        [Required]
        public string Name { get; set; }
    }
}