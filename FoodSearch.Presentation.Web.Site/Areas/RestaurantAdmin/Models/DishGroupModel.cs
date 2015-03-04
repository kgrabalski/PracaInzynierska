using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class DishGroupModel
    {
        [Required]
        public string Name { get; set; }
    }
}