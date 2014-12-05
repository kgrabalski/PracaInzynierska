
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class OpinionMap : ClassMap<Opinion>
    {
        public OpinionMap()
        {
            Table("Opinions");
            LazyLoad();
            Id(x => x.OpinionId).Column("OpinionId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            References(x => x.Restaurant).Column("RestaurantId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.Rating).Column("Rating").Not.Nullable();
            Map(x => x.Comment).Column("Comment").Not.Nullable();
            Map(x => x.UserId).Column("UserId").Not.Nullable();
            References(x => x.User).Column("UserId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
        }
    }
}
