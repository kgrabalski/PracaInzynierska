using System.Web.SessionState;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
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

        [Authorize]
        [HttpPost]
        public ActionResult GetDeliveryAddresses(UserInfo ui)
        {
            var addresses = _domain.User.GetUserDeliveryAddresses(ui.UserId);
            return Json(addresses, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Blank()
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Restaurants(int addressId, int? deliveryAddressId, Basket basket)
        {
            basket.AddressId = addressId;
            basket.DeliveryAddressId = deliveryAddressId;

            var restaurants = new RestaurantsListModel()
            {
                AddressId = addressId,
                Restaurants = _domain.Restaurant.GetRestaurants(addressId, DateTime.Now),
                Cuisines = _domain.RestaurantAdmin.GetCuisines()
            };
            return View(restaurants);
        }

        [HttpPost]
        public ActionResult GetRestaurants(int addressId, int[] cuisineId)
        {
            RestaurantFilter filter = null;
            if (cuisineId != null)
            {
                filter = new RestaurantFilter() {Cuisines = cuisineId};
            }
            var restaurants = _domain.Restaurant.GetRestaurants(addressId, DateTime.Now, filter);
            return Json(restaurants, JsonRequestBehavior.DenyGet);
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
                string restaurantName = _domain.Restaurant.GetRestaurantName(restaurantId.Value);
                if (string.IsNullOrEmpty(restaurantName)) return HttpNotFound();

                basket.CurrentRestaurant = restaurantId.Value;
                var dishes = _domain.Restaurant.GetDishes(restaurantId.Value);
                var model = new RestaurantDishesModel()
                {
                    RestaurantName = restaurantName,
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