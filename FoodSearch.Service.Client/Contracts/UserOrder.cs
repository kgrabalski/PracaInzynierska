using System;

namespace FoodSearch.Service.Client.Contracts
{
    public class UserOrder
    {
        public Guid OrderId { get; set; }
        public string CreateDate { get; set; }
        public string DeliveryType { get; set; }
        public string DeliveryAddress { get; set; }
        public string RestaurantName { get; set; }
        public Guid RestaurantId { get; set; }
        public string OrderAmount { get; set; }
    }
}

