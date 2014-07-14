using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class CuisinesController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public CuisinesController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public ActionResult GetCuisines()
        {
            var cuisines = _domain.RestaurantAdmin.GetCuisines();
            return Json(cuisines, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult GetRestaurantCuisines(RestaurantUser restUser)
        {
            var cuisines = _domain.RestaurantAdmin.GetRestaurantCuisines(restUser.RestaurantId);
            return Json(cuisines, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult AddRestaurantCuisine(RestaurantUser restUser, int cuisineId)
        {
            bool success = _domain.RestaurantAdmin.AddRestaurantCuisine(restUser.RestaurantId, cuisineId);
            return Json(success, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult RemoveRestaurantCuisine(RestaurantUser restUser, int cuisineId)
        {
            _domain.RestaurantAdmin.RemoveRestaurantCuisine(restUser.RestaurantId, cuisineId);
            return Json("ok", JsonRequestBehavior.DenyGet);
        }
    }
}