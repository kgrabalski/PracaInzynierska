using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantsController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index()
        {
            var restaurants = _domain.Restaurant.GetPartnerRestaurants();
            return View(restaurants);
        }

        public ActionResult Page(Guid? restaurantId)
        {
            if (!restaurantId.HasValue) return HttpNotFound();
            var model = new RestaurantPageModel
            {
                RestaurantDetails = _domain.Restaurant.GetRestaurantDetails(restaurantId.Value),
                RestaurantRating = _domain.Restaurant.GetRestaurantRating(restaurantId.Value),
                Opinions = _domain.Restaurant.GetOpinions(restaurantId.Value),
                OpeningHours = _domain.RestaurantAdmin.GetOpeningHours(restaurantId.Value),
                DishGroups = _domain.Restaurant.GetDishes(restaurantId.Value)
            };
            return View();
        }
    }
}