using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class OpinionFilterModel
    {
        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
        [Required]
        public int Page { get; set; }
    }
}
