using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class UserTypeMap : ClassMap<UserType>
    {
        public UserTypeMap()
        {
            Table("UserTypes");
            ReadOnly();
            LazyLoad();
            Id(x => x.UserTypeId).Column("UserTypeId").GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).Column("Name").Not.Nullable();
        }
    }
}
