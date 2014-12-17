using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class DishModel
    {
        [Required]
        public string DishName { get; set; }
        [Range(0, int.MaxValue)]
        public int DishGroupId { get; set; }
        [Range(0f, float.MaxValue)]
        public decimal Price { get; set; }
    }
}