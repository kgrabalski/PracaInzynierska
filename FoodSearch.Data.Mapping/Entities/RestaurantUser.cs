using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class RestaurantUser
    {
        public virtual int RestaurantUserId { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
    }           
}
