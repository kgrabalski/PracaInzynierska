using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class OpeningHoursController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public OpeningHoursController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public ActionResult GetOpeningHours(RestaurantUser restUser)
        {
            var openings = _domain.RestaurantAdmin.GetOpeningHours(restUser.RestaurantId);
            return Json(openings, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Create(RestaurantUser restUser, int day, string timeFrom, string timeTo)
        {
            TimeSpan tf = TimeSpan.ParseExact(timeFrom, @"hh\:mm", CultureInfo.InvariantCulture);
            TimeSpan tt = TimeSpan.ParseExact(timeTo, @"hh\:mm", CultureInfo.InvariantCulture);
            int openingId = _domain.RestaurantAdmin.CreateOpeningHour(restUser.RestaurantId, day, tf, tt);
            var opening = _domain.RestaurantAdmin.GetOpeningHours(restUser.RestaurantId, openingId);
            return Json(opening, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(int openingHourId)
        {
            _domain.RestaurantAdmin.DeleteOpeningHour(openingHourId);
            return Json("ok", JsonRequestBehavior.DenyGet);
        }
    }
}