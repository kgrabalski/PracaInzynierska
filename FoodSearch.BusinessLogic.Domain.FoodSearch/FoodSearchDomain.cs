using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order;
using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.BusinessLogic.Domain.User;
using FoodSearch.BusinessLogic.Domain.User.Interface;

using Ninject;

namespace FoodSearch.BusinessLogic.Domain.FoodSearch
{
    public class FoodSearchDomain : IFoodSearchDomain
    {
        private readonly IKernel _kernel;

        public FoodSearchDomain()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IOrderDomain>().To<OrderDomain>().InThreadScope();
            _kernel.Bind<IUserDomain>().To<UserDomain>().InThreadScope();
        }

        public IOrderDomain Order { get { return _kernel.Get<IOrderDomain>(); } }
        public IUserDomain User { get { return _kernel.Get<IUserDomain>(); } }
    }
}
