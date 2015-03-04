using System;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class UserOrder
    {
        public virtual Guid OrderId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string DeliveryType { get; set; }
        public virtual string DeliveryAddress { get; set; }
        public virtual string RestaurantName { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual decimal OrderAmount { get; set; }
    }
}
