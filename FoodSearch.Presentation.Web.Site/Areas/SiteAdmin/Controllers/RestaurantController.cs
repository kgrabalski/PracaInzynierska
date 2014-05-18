﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class RestaurantController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult GetLogo(int logoId)
        {
            var logo = _domain.Core.GetImage(logoId);
            return File(logo.ImageData, logo.ContentType);
        }

        [HttpPost]
        public ActionResult GetRestaurants()
        {
            var restaurants = _domain.SiteAdmin.GetRestaurants();
            return Json(restaurants, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid restaurantId)
        {
            _domain.SiteAdmin.DeleteRestaurant(restaurantId);
            return Json("ok", JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult UploadLogo()
        {
            var logo = Request.Files["logo"] as HttpPostedFileBase;
            Int32 length = logo.ContentLength;
            byte[] tempLogo = new byte[length];
            logo.InputStream.Read(tempLogo, 0, length);
            var logoId = _domain.Core.AddImage(tempLogo, Request.Files["logo"].ContentType);
            return Json(logoId, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Create(string name, int addressId, string userName, string password, int logoId)
        {
            var restaurantId = _domain.SiteAdmin.CreateRestaurant(name, addressId, logoId);
            var userId = _domain.RestaurantAdmin.CreateUser(restaurantId, userName, "Właściel", "Restauracji", "email", password, UserTypes.RestaurantAdmin);
            var restaurant = _domain.SiteAdmin.GetRestaurants(restaurantId);
            return Json(restaurant, JsonRequestBehavior.DenyGet);
        }
    }
}