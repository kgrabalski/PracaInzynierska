using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    internal class OrderStateHistoryMap : ClassMap<OrderStateHistory>
    {
        public OrderStateHistoryMap()
        {
            Table("OrderStatesHistory");
            LazyLoad();
            Id(x => x.OrderStateHistoryId).Column("OrderStateHistoryId").Not.Nullable().GeneratedBy.Identity();
            Map(x => x.OrderId).Column("OrderId").Not.Nullable();
            References(x => x.Order).Column("OrderId").LazyLoad().Not.Insert().Not.Update();
            Map(x => x.OrderStateId).Column("OrderStateId").Not.Nullable();
            References(x => x.OrderState).Column("OrderStateId").LazyLoad().Not.Insert().Not.Update();
            Map(x => x.ModificationDate).Column("ModificationDate").Not.Nullable();
        }
    }
}
