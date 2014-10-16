using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.Order.Models
{
    public class OrderItem
    {
        public int DishId { get; set; }
        public int Quantity { get; set; }
    }
}
