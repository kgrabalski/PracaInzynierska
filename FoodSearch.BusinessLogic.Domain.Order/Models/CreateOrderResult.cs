using System;

namespace FoodSearch.BusinessLogic.Domain.Order.Models
{
    public class CreateOrderResult
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string RestaurantName { get; set; }
        public decimal Price { get; set; }
    }
}
