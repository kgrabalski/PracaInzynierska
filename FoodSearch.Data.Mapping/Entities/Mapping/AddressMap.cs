using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Table("Addresses");
            LazyLoad();
            Id(x => x.AddressId).Column("AddressId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.DistrictId).Column("DistrictId").Not.Nullable();
            References(x => x.District).Column("DistrictId").LazyLoad().Not.Nullable();
            Map(x => x.StreetId).Column("StreetId").Not.Nullable();
            References(x => x.Street).Column("StreetId").LazyLoad().Not.Nullable();
            Map(x => x.Number).Column("Number").Not.Nullable();
            Map(x => x.Lat).Column("Lat").Not.Nullable();
            Map(x => x.Lon).Column("Lon").Not.Nullable();
            Map(x => x.CityId).Column("CityId").Not.Nullable();
            References(x => x.City).Column("CityId").LazyLoad().Not.Nullable();
        }
    }
}
