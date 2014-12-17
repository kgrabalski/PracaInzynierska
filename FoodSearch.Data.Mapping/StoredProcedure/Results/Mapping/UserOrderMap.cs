using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    public class UserOrderMap : ClassMap<UserOrder>
    {
        public UserOrderMap()
        {
            Table("GetUserOrders");
            ReadOnly();
            Id(x => x.OrderId).Column("OrderId").Not.Nullable();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
            Map(x => x.DeliveryType).Column("DeliveryType").Not.Nullable();
            Map(x => x.DeliveryAddress).Column("DeliveryAddress").Not.Nullable();
            Map(x => x.RestaurantName).Column("RestaurantName").Not.Nullable();
            Map(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            Map(x => x.OrderAmount).Column("OrderAmount").Not.Nullable();
        }
    }
}
