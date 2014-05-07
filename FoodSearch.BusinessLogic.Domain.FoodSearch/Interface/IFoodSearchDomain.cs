using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.BusinessLogic.Domain.User.Interface;

namespace FoodSearch.BusinessLogic.Domain.FoodSearch.Interface
{
    public interface IFoodSearchDomain
    {
        ICoreDomain Core { get; }
        IOrderDomain Order { get; }
        IUserDomain User { get; }
        IRestaurantAdminDomain RestaurantAdmin { get; }
        ISiteAdminDomain SiteAdmin { get; }
    }
}
