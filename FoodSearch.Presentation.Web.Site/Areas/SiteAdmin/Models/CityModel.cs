using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Models
{
    public class CityModel
    {
        [Required]
        public string Name { get; set; }
    }
}