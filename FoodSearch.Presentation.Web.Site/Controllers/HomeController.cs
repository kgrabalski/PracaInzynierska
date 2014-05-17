using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

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
    }
}