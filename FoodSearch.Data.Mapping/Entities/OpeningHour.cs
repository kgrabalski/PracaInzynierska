using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class OpeningHour
    {
        public virtual int OpeningId { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual int Day { get; set; }
        public virtual TimeSpan TimeFrom { get; set; }
        public virtual TimeSpan TimeTo { get; set; }
    }
}
