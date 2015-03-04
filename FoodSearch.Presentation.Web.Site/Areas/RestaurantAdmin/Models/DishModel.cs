using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class DishModel
    {
        [Required]
        public string DishName { get; set; }
        [Range(1, int.MaxValue)]
        public int DishGroupId { get; set; }
        [Range(0f, float.MaxValue)]
        public decimal Price { get; set; }
    }
}