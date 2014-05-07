using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class PaymentHistoryMap : ClassMap<PaymentHistory>
    {
        public PaymentHistoryMap()
        {
            Table("PaymentsHistory");
            LazyLoad();
            Id(x => x.PaymentHistoryId).Column("PaymentHistoryId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.PaymentId).Column("PaymentId").Not.Nullable();
            References(x => x.Payment).Column("PaymentId").LazyLoad().Not.Nullable();
            Map(x => x.PaymentStateId).Column("PaymentStateId").Not.Nullable();
            References(x => x.PaymentState).Column("PaymentStateId").LazyLoad().Not.Nullable();
            Map(x => x.ModificationDate).Column("ModificationDate").Not.Nullable();
        }
    }
}
