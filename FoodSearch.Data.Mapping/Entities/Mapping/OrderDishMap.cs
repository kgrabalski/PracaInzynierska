using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.Entities.Mapping
{
    //public class OrderDishMap : ClassMap<OrderDish>
    //{
    //    public OrderDishMap()
    //    {
    //        Table("OrderDishes");
    //        LazyLoad();
    //        CompositeId().KeyProperty(x => x.Order, "OrderId").KeyReference(x => x.Dish, "DishId");
    //        References(x => x.Order).Column("OrderId").Not.Nullable().LazyLoad();
    //        References(x => x.Dish).Column("DishId").Not.Nullable().LazyLoad();
    //    }
    //}
}
