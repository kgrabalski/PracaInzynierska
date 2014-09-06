
namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class OpeningHour
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string DayName { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
    }
}
