
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class StreetMap : ClassMap<Street>
    {
        public StreetMap()
        {
            Table("Streets");
            ReadOnly();
            LazyLoad();
            Id(x => x.StreetId).Column("StreetId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
