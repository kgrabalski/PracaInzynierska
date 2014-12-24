
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            LazyLoad();
            Id(x => x.UserId).Column("UserId").Not.Nullable().GeneratedBy.Guid();
            Map(x => x.FirstName).Column("FirstName").Not.Nullable();
            Map(x => x.LastName).Column("LastName").Not.Nullable();
            Map(x => x.Email).Column("Email").Not.Nullable();
            Map(x => x.Password).Column("Password").Not.Nullable();
            Map(x => x.UserTypeId).Column("UserTypeId").Not.Nullable();
            References(x => x.UserType).Column("UserTypeId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.UserStateId).Column("UserStateId").Not.Nullable();
            References(x => x.UserState).Column("UserStateId").LazyLoad().Not.Nullable().Not.Insert().Not.Update();
            Map(x => x.CreateDate).Column("CreateDate").Not.Nullable();
        }
    }
}
