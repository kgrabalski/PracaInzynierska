using System;
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


        public ActionResult Index()
        {
            var restaurants = _domain.SiteAdmin.GetRestaurants();
            return View(restaurants);
        }

        public ActionResult GetLogo(int logoId)
        {
            var logo = _domain.Core.GetImage(logoId);
            return File(logo.ImageData, logo.ContentType);
        }

        [HttpPost]
        public ActionResult Delete(Guid restaurantId)
        {
            _domain.SiteAdmin.DeleteRestaurant(restaurantId);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, int addressId, string userName, string password)
        {
            if (Request.Files.Count > 0)
            {
                var img = Request.Files["logo"] as HttpPostedFileBase;
                Int32 length = img.ContentLength;
                byte[] tempImage = new byte[length];
                img.InputStream.Read(tempImage, 0, length);

                int logoId = _domain.Core.AddImage(tempImage, Request.Files["logo"].ContentType);
                var id = _domain.SiteAdmin.CreateRestaurant(name, addressId, logoId);
                var userId = _domain.RestaurantAdmin.CreateUser(userName, "Właściel", "Restauracji", "email", password, UserTypes.RestaurantAdmin);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid restaurantId)
        {
            return View();
        }
    }
}