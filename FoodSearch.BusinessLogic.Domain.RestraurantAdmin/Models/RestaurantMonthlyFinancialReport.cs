
namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class RestaurantMonthlyFinancialReport
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
    }
}
