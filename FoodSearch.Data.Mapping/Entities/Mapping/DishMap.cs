using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Map(x => x.RestauraintId).Column("RestaurantId").Not.Nullable();
            References(x => x.Restaurant).Column("RestaurantId").LazyLoad().Not.Nullable().Not.Insert();
            Map(x => x.DishName).Column("DishName").Not.Nullable();
            Map(x => x.DishGroupId).Column("DishGroupId").Not.Nullable();
            References(x => x.DishGroup).Column("DishGroupId").Not.Nullable().Not.LazyLoad().Not.Insert().Fetch.Join();
            Map(x => x.Price).Column("Price").Not.Nullable();
        }
    }
}
