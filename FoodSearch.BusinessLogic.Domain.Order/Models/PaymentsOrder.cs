using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.Order.Models
{
    public class PaymentsOrder
    {
        public Guid OrderId { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
