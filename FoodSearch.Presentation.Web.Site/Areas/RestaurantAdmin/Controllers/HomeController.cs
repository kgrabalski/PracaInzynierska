using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using System.Web.Mvc;

using FoodSearch.Presentation.Web.Site.WebSocket;

using Microsoft.AspNet.SignalR;

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

        [HttpPost]
        public ActionResult GetRestaurantId(RestaurantUser restUser)
        {
            return Json(restUser.RestaurantId, JsonRequestBehavior.DenyGet);
        }
    }
}