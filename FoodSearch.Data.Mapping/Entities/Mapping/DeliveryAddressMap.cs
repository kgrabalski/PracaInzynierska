
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    internal class DeliveryAddressMap : ClassMap<DeliveryAddress>
    {
        public DeliveryAddressMap()
        {
            Table("DeliveryAddresses");
            LazyLoad();
            Id(x => x.DeliveryAddressId).Column("DeliveryAddressId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.UserId).Column("UserId").Not.Nullable();
            References(x => x.User).Column("UserId").Not.Nullable().LazyLoad().Not.Insert();
            Map(x => x.AddressId).Column("AddressId").Not.Nullable();
            References(x => x.Address).Column("AddressId").Not.Nullable().LazyLoad().Not.Insert();
            Map(x => x.FlatNumber).Column("FlatNumber").Not.Nullable();
        }
    }
}
