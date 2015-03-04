using System;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class RestaurantDeliveryRange
    {
        public virtual Guid RestaurantId { get; set; }
        public virtual string RestaurantName { get; set; }
        public virtual float Lat { get; set; }
        public virtual float Lon { get; set; }
        public virtual bool HasDeliveryRadius { get; set; }
        public virtual decimal DeliveryRadius { get; set; }
        public virtual string Polygon { get; set; }
    }
}
