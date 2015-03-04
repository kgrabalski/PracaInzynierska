using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class DeliveryAddress
    {
        public virtual int DeliveryAddressId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual string FlatNumber { get; set; }
    }
}
