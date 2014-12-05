using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.Restaurant.Models
{
    public class RestaurantRating
    {
        public Guid RestaurantId { get; set; }
        public int UsersVoted { get; set; }
        public float TotalRating { get; set; }
        public int StarsCount { get; set; }
        public int Percentage1Star { get; set; }
        public int Percentage2Stars { get; set; }
        public int Percentage3Stars { get; set; }
        public int Percentage4Stars { get; set; }
        public int Percentage5Stars { get; set; }
    }
}
