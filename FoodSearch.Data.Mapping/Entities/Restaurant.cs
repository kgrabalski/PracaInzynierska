using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class Restaurant
    {
        public virtual Guid RestaurantId { get; set; }
        public virtual int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual int ImageId { get; set; }
        public virtual Image Image { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal MinOrderAmount { get; set; }
        public virtual bool IsOpen { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual decimal DeliveryPrice { get; set; }
        public virtual decimal FreeDeliveryFrom { get; set; }
    }
}
