using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    internal class PasswordResetRequestMap : ClassMap<PasswordResetRequest>
    {
        public PasswordResetRequestMap()
        {
            Table("PasswordResetRequests");
            Id(x => x.RequestId).Column("RequestId").GeneratedBy.Guid().Not.Nullable();
            Map(x => x.UserId).Column("UserId").Not.Nullable();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
            Map(x => x.PasswordChanged).Column("PasswordChanged").Not.Nullable();
            Map(x => x.IsActive).Column("IsActive").Not.Nullable();
        }
    }
}
