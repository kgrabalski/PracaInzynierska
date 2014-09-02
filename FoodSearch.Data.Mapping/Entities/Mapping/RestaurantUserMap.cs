
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class RestaurantUserMap : ClassMap<RestaurantUser>
    {
        public RestaurantUserMap()
        {
            Table("RestaurantUsers");
            LazyLoad();
            Id(x => x.RestaurantUserId).Column("RestaurantUserId").Not.Nullable().GeneratedBy.Identity();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            References(x => x.Restaurant).Column("RestaurantId").Not.Nullable().LazyLoad().Not.Insert();
            Map(x => x.UserId).Column("UserId").Not.Nullable();
            References(x => x.User).Column("UserId").Not.Nullable().LazyLoad().Not.Insert();
        }
    }
}
