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
    public class DishesController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public DishesController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public ActionResult GetDishes(RestaurantUser restUser)
        {
            var dishes = _domain.RestaurantAdmin.GetDishes(restUser.RestaurantId);
            return Json(dishes, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Create(RestaurantUser restUser, string dishName, int dishGroupId, float price)
        {
            var dishId = _domain.RestaurantAdmin.CreateDish(restUser.RestaurantId, dishName, dishGroupId, price);
            var dish = _domain.RestaurantAdmin.GetDishes(restUser.RestaurantId, dishId);
            return Json(dish, JsonRequestBehavior.DenyGet);
        }
    }
}