using System.Collections.Generic;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class DeliveryRange
    {
        public bool HasDeliveryRadius { get; set; }
        public decimal DeliveryRadius { get; set; }
        public IEnumerable<DeliveryRangePoint> Polygon { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string RestaurantName { get; set; }
    }
}
