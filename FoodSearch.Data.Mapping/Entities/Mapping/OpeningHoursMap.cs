using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class OpeningHoursMap : ClassMap<OpeningHour>
    {
        public OpeningHoursMap()
        {
            Table("OpeningHours");
            LazyLoad();
            Id(x => x.OpeningId).Column("OpeningId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            References(x => x.Restaurant).Column("RestaurantId").LazyLoad().Not.Nullable();
            Map(x => x.Day).Column("Day").Not.Nullable();
            Map(x => x.TimeFrom).Column("TimeFrom").CustomType("TimeAsTimeSpan").Not.Nullable();
            Map(x => x.TimeTo).Column("TimeTo").CustomType("TimeAsTimeSpan").Not.Nullable();
        }
    }
}
