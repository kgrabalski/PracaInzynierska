using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class PaymentMap : ClassMap<Payment>
    {
        public PaymentMap()
        {
            Table("Payments");
            LazyLoad();
            Id(x => x.PaymentId).Column("PaymentId").GeneratedBy.Guid().Not.Nullable();
            Map(x => x.PaymentTypeId).Column("PaymentTypeId").Not.Nullable();
            References(x => x.PaymentType).Column("PaymentTypeId").LazyLoad().Not.Nullable();
            Map(x => x.Amount).Column("Amount").Not.Nullable();
            Map(x => x.PaymentStateId).Column("PaymentStateId").Not.Nullable();
            References(x => x.PaymentState).Column("PaymentStateId").LazyLoad().Not.Nullable();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
        }
    }
}
