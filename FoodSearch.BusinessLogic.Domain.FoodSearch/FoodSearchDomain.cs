using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FoodSearch.BusinessLogic.Domain.Core;
using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order;
using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.BusinessLogic.Domain.User;
using FoodSearch.BusinessLogic.Domain.User.Interface;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using Ninject;

namespace FoodSearch.BusinessLogic.Domain.FoodSearch
{
    public class FoodSearchDomain : IFoodSearchDomain
    {
        private readonly IKernel _kernel;

        public FoodSearchDomain()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IRepositoryHelper>().To<RepositoryHelper>();
            _kernel.Bind<ICoreDomain>().To<CoreDomain>().InThreadScope();
            _kernel.Bind<IOrderDomain>().To<OrderDomain>().InThreadScope();
            _kernel.Bind<IUserDomain>().To<UserDomain>().InThreadScope();
        }

        public ICoreDomain Core { get { return _kernel.Get<ICoreDomain>(); } }
        public IOrderDomain Order { get { return _kernel.Get<IOrderDomain>(); } }
        public IUserDomain User { get { return _kernel.Get<IUserDomain>(); } }
    }
}
