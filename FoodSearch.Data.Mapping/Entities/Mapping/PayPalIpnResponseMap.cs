using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

using NHibernate.Type;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    internal class PayPalIpnResponseMap : ClassMap<PayPalIpnResponse>
    {
        public PayPalIpnResponseMap()
        {
            Table("PayPalIpnResponses");
            Id(x => x.ResponseId).Column("ResponseId").Not.Nullable().GeneratedBy.Identity();
            Map(x => x.PaymentId).Column("PaymentId").Nullable();
            Map(x => x.Status).Column("Status").Not.Nullable();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
        }
    }
}
