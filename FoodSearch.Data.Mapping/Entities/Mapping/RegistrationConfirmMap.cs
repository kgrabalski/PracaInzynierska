using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class RegistrationConfirmMap : ClassMap<RegistrationConfirm>
    {
        public RegistrationConfirmMap()
        {
            Table("RegistrationConfirmations");
            LazyLoad();
            Id(x => x.ConfirmId).Column("ConfirmId").Not.Nullable().GeneratedBy.Identity();
            Map(x => x.Code).Column("Code").Not.Nullable();
            Map(x => x.Confirmed).Column("Confirmed").Not.Nullable();
            Map(x => x.UserId).Column("UserId").Not.Nullable();
            References(x => x.User).Column("UserId").Not.Nullable().LazyLoad().Not.Insert();
        }
    }
}
