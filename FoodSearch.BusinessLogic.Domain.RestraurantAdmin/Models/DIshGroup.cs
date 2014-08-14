using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class DishGroup
    {
        public int DishGroupId { get; set; }
        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
    }
}
