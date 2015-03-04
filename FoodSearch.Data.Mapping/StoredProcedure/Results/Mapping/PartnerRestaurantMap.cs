
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    internal class PartnerRestaurantMap : ClassMap<PartnerRestaurant>
    {
        public PartnerRestaurantMap()
        {
            Table("GetPartnerRestaurants");
            ReadOnly();
            Id(x => x.RestaurantId).Column("RestaurantId");
            Map(x => x.RestaurantName).Column("RestaurantName");
            Map(x => x.LogoId).Column("LogoId");
            Map(x => x.Street).Column("Street");
            Map(x => x.Number).Column("Number");
            Map(x => x.RestaurantRating).Column("RestaurantRating");
            Map(x => x.UsersVoted).Column("UsersVoted");
        }
    }
}
