using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class RestaurantRating
    {
        public virtual Guid RestaurantId { get; set; }
        public virtual int UsersVoted { get; set; }
        public virtual string TotalRating { get; set; }
        public virtual int StarsCount { get; set; }
        public virtual int Percentage1Star { get; set; }
        public virtual int Percentage2Stars { get; set; }
        public virtual int Percentage3Stars { get; set; }
        public virtual int Percentage4Stars { get; set; }
        public virtual int Percentage5Stars { get; set; }
    }
}
