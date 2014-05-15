using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class DishGroupMap : ClassMap<DishGroup>
    {
        public DishGroupMap()
        {
            Table("DishGroups");
            LazyLoad();
            Id(x => x.DishGroupId).Column("DishGroupId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
        }
    }
}
