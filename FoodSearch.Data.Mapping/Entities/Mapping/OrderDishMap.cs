
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    public class OrderDishMap : ClassMap<OrderDish>
    {
        public OrderDishMap()
        {
            Table("OrderDishes");
            LazyLoad();
            Id(x => x.OrderDishId).Column("OrderDishId").Not.Nullable().GeneratedBy.Identity();
            Map(x => x.OrderId).Column("OrderId").Not.Nullable();
            References(x => x.Order).Column("OrderId").Not.Nullable().LazyLoad().Not.Insert();
            Map(x => x.DishName).Column("DishName").Not.Nullable();
            Map(x => x.Price).Column("Price").Not.Nullable();
            Map(x => x.Quantity).Column("Quantity").Not.Nullable();
        }
    }
}
