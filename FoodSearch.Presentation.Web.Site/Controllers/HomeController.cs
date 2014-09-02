using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System;
using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
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
        public ActionResult GetStreets(string query = "")
        {
            var streets = _domain.Core.GetStreets(query);
            return Json(streets, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult GetStreetNumbers(int streetId)
        {
            var numbers = _domain.Core.GetStreetNumbers(streetId);
            return Json(numbers, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Restaurants(int addressId)
        {
            var restaurants = _domain.Core.GetRestaurants(addressId, DateTime.Now);
            return View(restaurants);
        }

        public ActionResult GetImage(int imageId)
        {
            var image = _domain.Core.GetImage(imageId);
            return File(image.ImageData, image.ContentType);
        }

        public ActionResult RestaurantDishes(Guid? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                var dishes = _domain.RestaurantAdmin.GetDishes(restaurantId.Value);
                return View(dishes);
            }
            return new EmptyResult();
        }
    }
}