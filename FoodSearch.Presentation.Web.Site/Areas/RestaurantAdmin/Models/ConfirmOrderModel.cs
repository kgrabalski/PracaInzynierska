using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class ConfirmOrderModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public string DeliveryTime { get; set; }
    }
}
