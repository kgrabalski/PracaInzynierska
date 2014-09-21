using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class PartnerRestaurant
    {
        public virtual Guid RestaurantId { get; set; }
        public virtual string RestaurantName { get; set; }
        public virtual int LogoId { get; set; }
        public virtual string Street { get; set; }
        public virtual string Number { get; set; }
        public virtual float RestaurantRating { get; set; }
        public virtual int UsersVoted { get; set; }
    }
}
