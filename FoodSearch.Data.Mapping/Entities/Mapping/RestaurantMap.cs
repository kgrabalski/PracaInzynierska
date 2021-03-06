﻿
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class RestaurantMap : ClassMap<Restaurant>
    {
        public RestaurantMap()
        {
            Table("Restaurant");
            LazyLoad();
            Id(x => x.RestaurantId).Column("RestaurantId").GeneratedBy.Guid().Not.Nullable();
            Map(x => x.AddressId).Column("AddressId").Not.Nullable();
            References(x => x.Address).Column("AddressId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.ImageId).Column("ImageId").Not.Nullable();
            References(x => x.Image).Column("ImageId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.MinOrderAmount).Column("MinOrderAmount").Not.Nullable();
            Map(x => x.IsOpen).Column("IsOpen").Not.Nullable();
            Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();
            Map(x => x.DeliveryPrice).Column("DeliveryPrice").Not.Nullable();
            Map(x => x.FreeDeliveryFrom).Column("FreeDeliveryFrom").Not.Nullable();
        }
    }
}
