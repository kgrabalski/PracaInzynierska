using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class AddressController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public AddressController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public ActionResult Cities()
        {
            var cities = _domain.Core.GetCities();
            return Json(cities, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Streets(int cityId, string query = "")
        {
            var streets = _domain.Core.GetStreets(cityId, query);
            return Json(streets, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult StreetNumbers(int streetId)
        {
            var numbers = _domain.Core.GetStreetNumbers(streetId);
            return Json(numbers, JsonRequestBehavior.DenyGet);
        }
    }
}