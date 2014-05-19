using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class RestaurantCuisineMap : ClassMap<RestaurantCuisine>
    {
        public RestaurantCuisineMap()
        {
            Table("RestaurantCuisines");
            LazyLoad();
            Id(x => x.RestaurantCuisineId).Column("RestaurantCuisineId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            References(x => x.Restaurant).Column("RestaurantId").Not.Nullable().Not.Insert().LazyLoad();
            Map(x => x.CuisineId).Column("CuisineId").Not.Nullable();
            References(x => x.Cuisine).Column("CuisineId").Not.Nullable().Not.LazyLoad().Not.Insert().Fetch.Join();
        }
    }
}
