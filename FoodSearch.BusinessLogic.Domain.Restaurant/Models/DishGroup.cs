using System.Collections.Generic;

namespace FoodSearch.BusinessLogic.Domain.Restaurant.Models
{
    public class DishGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }

        public DishGroup()
        {
            Dishes = new List<Dish>();
        }
    }
}
