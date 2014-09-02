using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class DishGroup
    {
        public virtual int DishGroupId { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual string Name { get; set; }
    }
}
