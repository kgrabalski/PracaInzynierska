using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
