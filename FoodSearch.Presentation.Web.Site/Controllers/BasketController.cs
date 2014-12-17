using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using FoodSearch.Presentation.Web.Site.Providers;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class BasketController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public BasketController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public ActionResult Add(int dishId, Basket basket)
        {
            var dish = _domain.Restaurant.GetDish(dishId);
            basket.AddItem(dish);
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        [HttpPost]
        public ActionResult Remove(int dishId, Basket basket)
        {
            basket.RemoveItem(dishId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult GetBasketItems(Basket basket)
        {
            basket.DeliveryPrice = _domain.Order.GetDeliveryPrice(basket.CurrentRestaurant, basket.Total);
            return Json(new
            {
                Total = basket.Total.ToPln(),
                DeliveryPrice = basket.DeliveryPrice.ToPln(),
                TotalWithDelivery = basket.TotalWithDelivery.ToPln(),
                Items = basket.Items.Select(x => new
                {
                    x.DishId,
                    x.Name,
                    x.Count,
                    Price = x.Price.ToPln(),
                    Total = x.Total.ToPln()
                })}, JsonRequestBehavior.DenyGet);
        }
    }
}