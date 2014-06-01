using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.Helpers;

using RestaurantUser = FoodSearch.Presentation.Web.Site.Models.RestaurantUser;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class HomeController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public HomeController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index(RestaurantUser restUser)
        {
            var data = _domain.RestaurantAdmin.GetEmployeeData(restUser.RestaurantId, restUser.UserId);
            return View(data);
        }

        [ChildActionOnly]
        public ActionResult UserMenu(RestaurantUser restUser)
        {
            var data = _domain.RestaurantAdmin.GetEmployeeData(restUser.RestaurantId, restUser.UserId);
            return PartialView("_UserMenu", data);
        }
    }
}