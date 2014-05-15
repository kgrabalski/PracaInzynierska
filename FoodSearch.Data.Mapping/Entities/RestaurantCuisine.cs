using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class RestaurantCuisine
    {
        public virtual int RestaurantCuisineId { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual int CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }
    }
}
