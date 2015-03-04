using System.ComponentModel.DataAnnotations;

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
