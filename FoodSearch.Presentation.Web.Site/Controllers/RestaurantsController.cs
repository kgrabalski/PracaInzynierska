using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

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

        public ActionResult Page(Guid restaurantId)
        {
            return View();
        }
    }
}