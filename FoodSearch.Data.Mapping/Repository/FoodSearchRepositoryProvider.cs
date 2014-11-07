
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;

using NHibernate;

using Ninject;

namespace FoodSearch.Data.Mapping.Repository
{
    public class FoodSearchRepositoryProvider : IRepositoryProvider
    {
        private readonly IKernel _kernel;
        private readonly ISessionSource _sessionSource;

        public FoodSearchRepositoryProvider(ISessionSource sessionSource)
        {
            _sessionSource = sessionSource;
            _kernel = new StandardKernel();
            _kernel.Bind<ISessionSource>().ToConstant(_sessionSource);
            _kernel.Bind<IRepository<Address>>().To<FoodSearchRepository<Address>>();
            _kernel.Bind<IRepository<City>>().To<FoodSearchRepository<City>>();
            _kernel.Bind<IRepository<Cuisine>>().To<FoodSearchRepository<Cuisine>>();
            _kernel.Bind<IRepository<DeliveryAddress>>().To<FoodSearchRepository<DeliveryAddress>>();
            _kernel.Bind<IRepository<DeliveryType>>().To<FoodSearchRepository<DeliveryType>>();
            _kernel.Bind<IRepository<Dish>>().To<FoodSearchRepository<Dish>>();
            _kernel.Bind<IRepository<DishGroup>>().To<FoodSearchRepository<DishGroup>>();
            _kernel.Bind<IRepository<District>>().To<FoodSearchRepository<District>>();
            _kernel.Bind<IRepository<Image>>().To<FoodSearchRepository<Image>>();
            _kernel.Bind<IRepository<OpeningHour>>().To<FoodSearchRepository<OpeningHour>>();
            _kernel.Bind<IRepository<Opinion>>().To<FoodSearchRepository<Opinion>>();
            _kernel.Bind<IRepository<Order>>().To<FoodSearchRepository<Order>>();
            _kernel.Bind<IRepository<OrderDish>>().To<FoodSearchRepository<OrderDish>>();
            _kernel.Bind<IRepository<PasswordResetRequest>>().To<FoodSearchRepository<PasswordResetRequest>>();
            _kernel.Bind<IRepository<Payment>>().To<FoodSearchRepository<Payment>>();
            _kernel.Bind<IRepository<PaymentHistory>>().To<FoodSearchRepository<PaymentHistory>>();
            _kernel.Bind<IRepository<PaymentState>>().To<FoodSearchRepository<PaymentState>>();
            _kernel.Bind<IRepository<PaymentType>>().To<FoodSearchRepository<PaymentType>>();
            _kernel.Bind<IRepository<PayPalIpnResponse>>().To<FoodSearchRepository<PayPalIpnResponse>>();
            _kernel.Bind<IRepository<RegistrationConfirm>>().To<FoodSearchRepository<RegistrationConfirm>>();
            _kernel.Bind<IRepository<Restaurant>>().To<FoodSearchRepository<Restaurant>>();
            _kernel.Bind<IRepository<RestaurantCuisine>>().To<FoodSearchRepository<RestaurantCuisine>>();
            _kernel.Bind<IRepository<RestaurantUser>>().To<FoodSearchRepository<RestaurantUser>>();
            _kernel.Bind<IRepository<Street>>().To<FoodSearchRepository<Street>>();
            _kernel.Bind<IRepository<User>>().To<FoodSearchRepository<User>>();
            _kernel.Bind<IRepository<UserState>>().To<FoodSearchRepository<UserState>>();
            _kernel.Bind<IRepository<UserType>>().To<FoodSearchRepository<UserType>>();
            _kernel.Bind<IStoredProcedureRepository>().To<FoodSearchStoredProcedureRepository>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _kernel.Get<IRepository<T>>();
        }

        public IStoredProcedureRepository StoredProcedure { get { return _kernel.Get<IStoredProcedureRepository>(); } }

        public ITransaction BeginTransaction
        {
            get { return _sessionSource.Session.BeginTransaction(); }
        }
    }
}
