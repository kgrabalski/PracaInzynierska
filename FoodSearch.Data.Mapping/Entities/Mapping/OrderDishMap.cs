﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Map(x => x.DishId).Column("DishId").Not.Nullable();
            References(x => x.Dish).Column("DishId").Not.Nullable().LazyLoad().Not.Insert();
        }
    }
}
