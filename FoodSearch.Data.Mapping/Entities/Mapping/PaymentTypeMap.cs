
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class PaymentTypeMap : ClassMap<PaymentType>
    {
        public PaymentTypeMap()
        {
            Table("PaymentTypes");
            ReadOnly();
            LazyLoad();
            Id(x => x.PaymentTypeId).Column("PaymentTypeId").Not.Nullable().GeneratedBy.Identity();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
