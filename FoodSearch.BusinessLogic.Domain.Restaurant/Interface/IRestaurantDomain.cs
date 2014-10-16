using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;

namespace FoodSearch.BusinessLogic.Domain.Restaurant.Interface
{
    public interface IRestaurantDomain
    {
        IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date);
        IEnumerable<DishGroup> GetDishes(Guid restaurantId);
        Dish GetDish(int dishId);
        IEnumerable<PartnerRestaurant> GetPartnerRestaurants();
        
        IEnumerable<Opinion> GetOpinions(Guid restaurantId, int page = 0, int pageSize = 10);
        RestaurantDetails GetRestaurantDetails(Guid restaurantId);
        RestaurantRating GetRestaurantRating(Guid restaurantId);
    }
}
