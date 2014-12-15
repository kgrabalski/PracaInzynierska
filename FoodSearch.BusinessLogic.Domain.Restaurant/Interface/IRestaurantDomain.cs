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
        IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date, RestaurantFilter filter = null);
        string GetRestaurantName(Guid restaurantId);
        IEnumerable<DishGroup> GetDishes(Guid restaurantId);
        Dish GetDish(int dishId);
        IEnumerable<PartnerRestaurant> GetPartnerRestaurants();
        bool AddOpinion(Guid restaurantId, Guid userId, int rating, string comment);
        IEnumerable<Opinion> GetOpinions(Guid restaurantId, int rating = 0, int page = 0);
        RestaurantDetails GetRestaurantDetails(Guid restaurantId);
        RestaurantRating GetRestaurantRating(Guid restaurantId);
        bool CheckUserCommentExists(Guid userId, Guid restaurantId);
    }
}
