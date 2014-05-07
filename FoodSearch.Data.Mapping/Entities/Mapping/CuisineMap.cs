using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class CuisineMap : ClassMap<Cuisine>
    {
        public CuisineMap()
        {
            Table("Cuisines");
            ReadOnly();
            LazyLoad();
            Id(x => x.CuisineId).Column("CuisineId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
