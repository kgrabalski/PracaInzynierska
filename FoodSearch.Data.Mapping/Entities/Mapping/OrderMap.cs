using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            References(x => x.User).Column("UserId").LazyLoad().Not.Nullable();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
            Map(x => x.DeliveryTypeId).Column("DeliveryTypeId").Not.Nullable();
            References(x => x.DeliveryType).Column("DeliveryTypeId").LazyLoad().Not.Nullable();
            Map(x => x.PaymentId).Column("PaymentId").Not.Nullable();
            References(x => x.Payment).Column("PaymentId").LazyLoad().Not.Nullable();
            Map(x => x.DeliveryData).Column("DeliveryData").Not.Nullable();
        }
    }
}
