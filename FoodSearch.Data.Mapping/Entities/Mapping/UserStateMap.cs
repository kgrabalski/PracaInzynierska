using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class UserStateMap : ClassMap<UserState>
    {
        public UserStateMap()
        {
            Table("UserStates");
            ReadOnly();
            LazyLoad();
            Id(x => x.UserStateId).Column("UserStateId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
