using System;

namespace FoodSearch.BusinessLogic.Domain.Order.Models
{
    public class PaymentsOrder
    {
        public Guid OrderId { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
