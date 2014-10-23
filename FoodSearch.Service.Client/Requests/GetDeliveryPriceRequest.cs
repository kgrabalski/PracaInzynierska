using System;

namespace FoodSearch.Service.Client.Requests
{
    public class GetDeliveryPriceRequest
    {
        public Guid RestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

