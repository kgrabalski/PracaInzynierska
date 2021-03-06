﻿
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class PaymentStateMap : ClassMap<PaymentState>
    {
        public PaymentStateMap()
        {
            Table("PaymentStates");
            ReadOnly();
            LazyLoad();
            Id(x => x.PaymentStateId).Column("PaymentStateId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
