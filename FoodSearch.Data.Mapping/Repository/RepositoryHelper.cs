using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;

using Ninject;

namespace FoodSearch.Data.Mapping.Repository
{
    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly IKernel _kernel;

        public RepositoryHelper()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IRepository<Address>>().To<FoodSearchRepository<Address>>();
            _kernel.Bind<IRepository<City>>().To<FoodSearchRepository<City>>();
            _kernel.Bind<IRepository<Cuisine>>().To<FoodSearchRepository<Cuisine>>();
            _kernel.Bind<IRepository<DeliveryType>>().To<FoodSearchRepository<DeliveryType>>();
            _kernel.Bind<IRepository<Dish>>().To<FoodSearchRepository<Dish>>();
            _kernel.Bind<IRepository<DishGroup>>().To<FoodSearchRepository<DishGroup>>();
            _kernel.Bind<IRepository<District>>().To<FoodSearchRepository<District>>();
            _kernel.Bind<IRepository<Image>>().To<FoodSearchRepository<Image>>();
            _kernel.Bind<IRepository<OpeningHour>>().To<FoodSearchRepository<OpeningHour>>();
            _kernel.Bind<IRepository<Opinion>>().To<FoodSearchRepository<Opinion>>();
            _kernel.Bind<IRepository<Order>>().To<FoodSearchRepository<Order>>();
            _kernel.Bind<IRepository<OrderDish>>().To<FoodSearchRepository<OrderDish>>();
            _kernel.Bind<IRepository<Payment>>().To<FoodSearchRepository<Payment>>();
            _kernel.Bind<IRepository<PaymentHistory>>().To<FoodSearchRepository<PaymentHistory>>();
            _kernel.Bind<IRepository<PaymentState>>().To<FoodSearchRepository<PaymentState>>();
            _kernel.Bind<IRepository<PaymentType>>().To<FoodSearchRepository<PaymentType>>();
            _kernel.Bind<IRepository<Restaurant>>().To<FoodSearchRepository<Restaurant>>();
            _kernel.Bind<IRepository<RestaurantCuisine>>().To<FoodSearchRepository<RestaurantCuisine>>();
            _kernel.Bind<IRepository<Street>>().To<FoodSearchRepository<Street>>();
            _kernel.Bind<IRepository<User>>().To<FoodSearchRepository<User>>();
            _kernel.Bind<IRepository<UserState>>().To<FoodSearchRepository<UserState>>();
            _kernel.Bind<IRepository<UserType>>().To<FoodSearchRepository<UserType>>();
        }

        public IRepository<Address> AddressRepository { get { return _kernel.Get<IRepository<Address>>(); } }
        public IRepository<City> CityRepository { get { return _kernel.Get<IRepository<City>>(); } }
        public IRepository<Cuisine> CuisineRepository { get { return _kernel.Get<IRepository<Cuisine>>(); } }
        public IRepository<DeliveryType> DeliveryTypeRepository { get { return _kernel.Get<IRepository<DeliveryType>>(); } }
        public IRepository<Dish> DishRepository { get { return _kernel.Get<IRepository<Dish>>(); } }
        public IRepository<DishGroup> DishGroupRepository { get { return _kernel.Get<IRepository<DishGroup>>(); } }
        public IRepository<District> DistrictRepository { get { return _kernel.Get<IRepository<District>>(); } }
        public IRepository<Image> ImageRepository { get { return _kernel.Get<IRepository<Image>>(); } }
        public IRepository<OpeningHour> OpeningHourRepository { get { return _kernel.Get<IRepository<OpeningHour>>(); } }
        public IRepository<Opinion> OpinionRepository { get { return _kernel.Get<IRepository<Opinion>>(); } }
        public IRepository<Order> OrderRepository { get { return _kernel.Get<IRepository<Order>>(); } }
        public IRepository<OrderDish> OrderDishRepository { get { return _kernel.Get<IRepository<OrderDish>>(); } }
        public IRepository<Payment> PaymentRepository { get { return _kernel.Get<IRepository<Payment>>(); } }
        public IRepository<PaymentHistory> PaymentHistoryRepository { get { return _kernel.Get<IRepository<PaymentHistory>>(); } }
        public IRepository<PaymentState> PaymentStateRepository { get { return _kernel.Get<IRepository<PaymentState>>(); } }
        public IRepository<PaymentType> PaymentTypeRepository { get { return _kernel.Get<IRepository<PaymentType>>(); } }
        public IRepository<Restaurant> RestaurantRepository { get { return _kernel.Get<IRepository<Restaurant>>(); } }
        public IRepository<RestaurantCuisine> RestaurantCuisineRepository { get { return _kernel.Get<IRepository<RestaurantCuisine>>(); } }
        public IRepository<Street> StreetRepository { get { return _kernel.Get<IRepository<Street>>(); } }
        public IRepository<User> UserRepository { get { return _kernel.Get<IRepository<User>>(); } }
        public IRepository<UserState> UserStateRepository { get { return _kernel.Get<IRepository<UserState>>(); } }
        public IRepository<UserType> UserTypeRepository { get { return _kernel.Get<IRepository<UserType>>(); } }
    }
}
