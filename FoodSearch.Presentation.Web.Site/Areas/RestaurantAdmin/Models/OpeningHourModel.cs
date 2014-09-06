using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class OpeningHourModel
    {
        [Range(1, 7)]
        public int Day { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
    }
}