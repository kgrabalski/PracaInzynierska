
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class DistrictMap : ClassMap<District>
    {
        public DistrictMap()
        {
            Table("Districts");
            ReadOnly();
            LazyLoad();
            Id(x => x.DistrictId).Column("DistrictId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.CityId).Column("CityId").Not.Nullable();
            References(x => x.City).Column("CityId").Not.Insert().LazyLoad();
        }
    }
}
