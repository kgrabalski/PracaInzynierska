using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class Opinion
    {
        public virtual int OpinionId { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual short Rating { get; set; }
        public virtual string Comment { get; set; }
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
