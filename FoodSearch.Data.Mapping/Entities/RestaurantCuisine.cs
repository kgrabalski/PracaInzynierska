using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class RestaurantCuisine
    {
        public virtual int RestaurantId { get; set; }
        public virtual int CuisineId { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as RestaurantCuisine;
            if (t == null)
                return false;
            if (RestaurantId == t.RestaurantId && CuisineId == t.CuisineId)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return (RestaurantId + "|" + CuisineId).GetHashCode();
        }
    }
}
