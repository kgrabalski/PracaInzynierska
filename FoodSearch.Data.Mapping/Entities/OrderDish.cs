using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class OrderDish
    {
        public virtual int OrderDishId { get; set; }
        public virtual Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual string DishName { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
    }
}
