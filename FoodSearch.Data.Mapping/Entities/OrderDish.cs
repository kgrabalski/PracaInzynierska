using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class OrderDish
    {
        public virtual int OrderDishId { get; set; }
        public virtual Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual int DishId { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
