using System;

namespace FoodSearch.Service.Client.Requests
{
    public class CreateOrderRequest
    {
        public int PaymentTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
    }
}

