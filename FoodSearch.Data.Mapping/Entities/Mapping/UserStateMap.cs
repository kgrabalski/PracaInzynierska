
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
