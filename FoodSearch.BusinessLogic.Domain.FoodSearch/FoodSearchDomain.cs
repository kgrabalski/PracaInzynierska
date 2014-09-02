
using FoodSearch.BusinessLogic.Domain.Core;
using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order;
using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface;
using FoodSearch.BusinessLogic.Domain.SiteAdmin;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
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
#if DEBUG
            _kernel.Bind<ISessionSource>().To<SessionSource>()
                .InSingletonScope()
                .WithConstructorArgument("db", Databases.FoodSearchTest);
#else
            _kernel.Bind<ISessionSource>().To<SessionSource>()
                .InSingletonScope()
                .WithConstructorArgument("db", Databases.FoodSeach);
#endif
            _kernel.Bind<IRepositoryProvider>().To<FoodSearchRepositoryProvider>();
            _kernel.Bind<ICoreDomain>().To<CoreDomain>().InThreadScope();
            _kernel.Bind<IOrderDomain>().To<OrderDomain>().InThreadScope();
            _kernel.Bind<IUserDomain>().To<UserDomain>().InThreadScope();
            _kernel.Bind<IRestaurantAdminDomain>().To<RestaurantAdminDomain>().InThreadScope();
            _kernel.Bind<ISiteAdminDomain>().To<SiteAdminDomain>().InThreadScope();
        }

        public ICoreDomain Core { get { return _kernel.Get<ICoreDomain>(); } }
        public IOrderDomain Order { get { return _kernel.Get<IOrderDomain>(); } }
        public IUserDomain User { get { return _kernel.Get<IUserDomain>(); } }
        public IRestaurantAdminDomain RestaurantAdmin { get { return _kernel.Get<IRestaurantAdminDomain>(); } }
        public ISiteAdminDomain SiteAdmin { get { return _kernel.Get<ISiteAdminDomain>(); } }
    }
}
