using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

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
        public ActionResult Add(int dishId, string returnUrl, Basket basket)
        {
            var dish = _domain.Restaurant.GetDish(dishId);
            basket.AddItem(dish);
            return RedirectBack(returnUrl);
        }

        [HttpPost]
        public ActionResult Remove(int dishId, string returnUrl, Basket basket)
        {
            basket.RemoveItem(dishId);
            return RedirectBack(returnUrl);
        }

        private ActionResult RedirectBack(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index(Basket basket)
        {
            if (basket.CurrentRestaurant != Guid.Empty) 
                basket.DeliveryPrice = _domain.Restaurant.GetDeliveryPrice(basket.CurrentRestaurant, basket.Total);
            return View(basket);
        }
    }
}