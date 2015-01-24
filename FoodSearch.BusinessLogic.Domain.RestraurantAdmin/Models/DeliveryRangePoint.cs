namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public struct DeliveryRangePoint
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public DeliveryRangePoint(double lat, double lon) : this()
        {
            Lat = lat;
            Lon = lon;
        }
    }
}