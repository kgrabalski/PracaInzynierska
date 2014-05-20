using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class DishGroup
    {
        public virtual int DishGroupId { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual string Name { get; set; }
    }
}
