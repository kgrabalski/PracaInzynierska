using System;

namespace FoodSearch.Service.Api.Areas.Order.Models
{
    public class DeliveryPriceModel
    {
        public Guid RestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
