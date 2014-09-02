using System;

namespace FoodSearch.Service.Contracts.Response
{
    public class RestaurantInfo
    {
        public Guid RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int LogoId { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
    }
}
