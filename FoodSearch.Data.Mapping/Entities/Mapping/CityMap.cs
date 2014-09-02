
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Table("Cities");
            ReadOnly();
            LazyLoad();
            Id(x => x.CityId).Column("CityId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name");
        }
    }
}
