using System;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class RestaurantInfo
    {
        public virtual Guid RestaurantId { get; set; }
        public virtual string RestaurantName { get; set; }
        public virtual int LogoId { get; set; }
        public virtual string TimeFrom { get; set; }
        public virtual string TimeTo { get; set; }
        public virtual float RestaurantRating { get; set; }
        public virtual int UsersVoted { get; set; }

    }
}
