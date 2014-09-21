using System.Web.SessionState;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class HomeController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public HomeController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Restaurants(int addressId)
        {
            var restaurants = _domain.Restaurant.GetRestaurants(addressId, DateTime.Now);
            return View(restaurants);
        }

        public ActionResult GetImage(int imageId)
        {
            var image = _domain.Core.GetImage(imageId);
            return File(image.ImageData, image.ContentType);
        }

        public ActionResult RestaurantDishes(Guid? restaurantId, Basket basket)
        {
            if (restaurantId.HasValue)
            {
                basket.CurrentRestaurant = restaurantId.Value;
                var dishes = _domain.Restaurant.GetDishes(restaurantId.Value);
                var model = new RestaurantDishesModel()
                {
                    DishGroups = dishes,
                    Basket = basket
                };
                return View(model);
            }
            return HttpNotFound();
        }

        [ChildActionOnly]
        public ActionResult UserLinks(UserInfo ui)
        {
            UserDetails details = new UserDetails();
            if (User.Identity.IsAuthenticated)
            {
                details = _domain.User.GetUserDetails(ui.UserId);
            }
            return PartialView("_UserLinks", details);
        }
    }
}