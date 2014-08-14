using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public int DishGroupId { get; set; }
        public string DishGroup { get; set; }
        public string Price { get; set; }
    }
}
