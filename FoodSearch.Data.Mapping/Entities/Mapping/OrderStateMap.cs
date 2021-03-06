﻿
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    internal class OrderStateMap : ClassMap<OrderState>
    {
        public OrderStateMap()
        {
            Table("OrderState");
            Id(x => x.OrderStateId).Column("OrderStateId").Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
