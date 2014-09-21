
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    public class RestaurantInfoMap : ClassMap<RestaurantInfo>
    {
        public RestaurantInfoMap()
        {
            Table("GetRestaurants");
            ReadOnly();
            Id(x => x.RestaurantId).Column("RestaurantId");
            Map(x => x.RestaurantName).Column("RestaurantName");
            Map(x => x.LogoId).Column("LogoId");
            Map(x => x.TimeFrom).Column("TimeFrom");
            Map(x => x.TimeTo).Column("TimeTo");
            Map(x => x.Street).Column("Street");
            Map(x => x.Number).Column("Number");
            Map(x => x.MinimumOrder).Column("MinimumOrder");
            Map(x => x.RestaurantRating).Column("RestaurantRating");
            Map(x => x.UsersVoted).Column("UsersVoted");
        }
    }
}
