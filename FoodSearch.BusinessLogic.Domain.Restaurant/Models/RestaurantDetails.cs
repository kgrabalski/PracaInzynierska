using System;

namespace FoodSearch.BusinessLogic.Domain.Restaurant.Models
{
    public class RestaurantDetails
    {
        public Guid RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int LogoId { get; set; }
        public int AddressId { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public decimal MinimumOrder { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FreeDeliveryFrom { get; set; }
    }
}
