using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.Entities;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface IRepositoryHelper
    {
        IRepository<Address> AddressRepository { get; }
        IRepository<City> CityRepository { get; }
        IRepository<Cuisine> CuisineRepository { get; }
        IRepository<DeliveryType> DeliveryTypeRepository { get; }
        IRepository<Dish> DishRepository { get; }
        IRepository<DishGroup> DishGroupRepository { get; }
        IRepository<District> DistrictRepository { get; }
        IRepository<Image> ImageRepository { get; }
        IRepository<OpeningHour> OpeningHourRepository { get; }
        IRepository<Opinion> OpinionRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderDish> OrderDishRepository { get; }
        IRepository<Payment> PaymentRepository { get; }
        IRepository<PaymentHistory> PaymentHistoryRepository { get; }
        IRepository<PaymentState> PaymentStateRepository { get; }
        IRepository<PaymentType> PaymentTypeRepository { get; }
        IRepository<Restaurant> RestaurantRepository { get; }
        IRepository<RestaurantCuisine> RestaurantCuisineRepository { get; }
        IRepository<Street> StreetRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<UserState> UserStateRepository { get; }
        IRepository<UserType> UserTypeRepository { get; }
    }
}
