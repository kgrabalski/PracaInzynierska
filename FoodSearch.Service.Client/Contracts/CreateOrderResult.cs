using System;

namespace FoodSearch.Service.Client.Contracts
{
    public class CreateOrderResult
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string RestaurantName { get; set; }
        public decimal Price { get; set; }
        public bool Succeed { get; set; }
    }
}

