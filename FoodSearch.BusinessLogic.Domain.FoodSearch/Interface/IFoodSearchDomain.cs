using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.BusinessLogic.Domain.User.Interface;

namespace FoodSearch.BusinessLogic.Domain.FoodSearch.Interface
{
    public interface IFoodSearchDomain
    {
        IOrderDomain Order { get; }
        IUserDomain User { get; }
    }
}
