using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Models
{
    public class RestaurantModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int AddressId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}