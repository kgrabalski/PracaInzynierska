﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using NHibernate.Criterion;
using NHibernate.Impl;

namespace FoodSearch.BusinessLogic.Domain.Order
{
    public class OrderDomain : IOrderDomain
    {
        private readonly IRepositoryProvider _provider;

        public OrderDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }
    }
}
