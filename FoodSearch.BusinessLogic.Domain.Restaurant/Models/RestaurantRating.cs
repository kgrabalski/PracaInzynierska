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
        public float Percentage1Star { get; set; }
        public float Percentage2Stars { get; set; }
        public float Percentage3Stars { get; set; }
        public float Percentage4Stars { get; set; }
        public float Percentage5Stars { get; set; }
    }
}
