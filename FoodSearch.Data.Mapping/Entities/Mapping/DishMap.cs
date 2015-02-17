
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class DishMap : ClassMap<Dish>
    {
        public DishMap()
        {
            Table("Dishes");
            LazyLoad();
            Id(x => x.DishId).Column("DishId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            References(x => x.Restaurant).Column("RestaurantId").LazyLoad().Not.Insert().Not.Update();
            Map(x => x.DishName).Column("DishName").Not.Nullable();
            Map(x => x.DishGroupId).Column("DishGroupId").Not.Nullable();
            References(x => x.DishGroup).Column("DishGroupId").Not.LazyLoad().Not.Insert().Not.Update();
            Map(x => x.Price).Column("Price").Not.Nullable();
            Map(x => x.ImageId).Column("ImageId").Not.Nullable();
        }
    }
}
