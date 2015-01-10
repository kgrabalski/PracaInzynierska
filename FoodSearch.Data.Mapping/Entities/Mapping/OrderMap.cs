
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Orders");
            LazyLoad();
            Id(x => x.OrderId).Column("OrderId").GeneratedBy.Guid().Not.Nullable();
            Map(x => x.UserId).Column("UserId").Not.Nullable();
            References(x => x.User).Column("UserId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
            Map(x => x.DeliveryTypeId).Column("DeliveryTypeId").Not.Nullable();
            References(x => x.DeliveryType).Column("DeliveryTypeId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.DeliveryData).Column("DeliveryData").Not.Nullable();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            References(x => x.Restaurant).Column("RestaurantId").Not.Insert().Not.Update().LazyLoad();
            Map(x => x.OrderStateId).Column("OrderStateId").Not.Nullable();
            References(x => x.OrderState).Column("OrderStateId").Not.Insert().Not.Update().LazyLoad();
        }
    }
}
