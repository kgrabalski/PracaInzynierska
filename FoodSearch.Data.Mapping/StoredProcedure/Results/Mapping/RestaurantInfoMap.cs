using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
