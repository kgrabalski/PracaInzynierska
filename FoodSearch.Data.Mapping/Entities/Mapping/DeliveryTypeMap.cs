using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class DeliveryTypeMap : ClassMap<DeliveryType>
    {
        public DeliveryTypeMap()
        {
            Table("DeliveryTypes");
            LazyLoad();
            ReadOnly();
            Id(x => x.DeliveryTypeId).Column("DeliveryTypeId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
