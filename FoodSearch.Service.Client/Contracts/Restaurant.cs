using System;

namespace FoodSearch.Service.Client.Contracts
{
    public class Restaurant
    {
        public Guid RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int LogoId { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public float MinimumOrder { get; set; }
        public float RestaurantRating { get; set; }
        public int StarsCount { get; set; }
        public int UsersVoted { get; set; }
        public override string ToString()
        {
            return RestaurantName;
        }
        public string Address { get { return string.Format("{0} {1}", Street, Number); } }
        public string Openings { get { return string.Format("Otwarta od: {0} do: {1}", TimeFrom, TimeTo); } }
    }
}
