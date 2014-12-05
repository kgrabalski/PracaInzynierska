using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class OpinionModel
    {
        [Required]
        public Guid RestaurantId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
