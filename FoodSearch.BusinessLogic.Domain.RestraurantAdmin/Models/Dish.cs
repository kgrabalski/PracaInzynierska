
namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public int DishGroupId { get; set; }
        public string DishGroup { get; set; }
        public string Price { get; set; }
        public int ImageId { get; set; }
    }
}
