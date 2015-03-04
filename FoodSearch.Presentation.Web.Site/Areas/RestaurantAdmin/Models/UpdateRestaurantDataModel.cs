
namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class UpdateRestaurantDataModel
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FreeDeliveryFrom { get; set; }
    }
}
