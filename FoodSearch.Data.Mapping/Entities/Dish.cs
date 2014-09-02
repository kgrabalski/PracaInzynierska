using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class Dish
    {
        public virtual int DishId { get; set; }
        public virtual Guid RestauraintId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual string DishName { get; set; }
        public virtual int DishGroupId { get; set; }
        public virtual DishGroup DishGroup { get; set; }
        public virtual float Price { get; set; }
    }
}
