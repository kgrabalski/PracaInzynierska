using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class OrderDish
    {
        // public virtual Guid OrderId { get; set; }
        public Order Order { get; set; }
        //public virtual int DishId { get; set; }
        public Dish Dish { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as OrderDish;
            if (t == null)
                return false;
            if (Order.OrderId == t.Order.OrderId && Dish.DishId == t.Dish.DishId)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return (Order.OrderId + "|" + Dish.DishId).GetHashCode();
        }
    }
}
