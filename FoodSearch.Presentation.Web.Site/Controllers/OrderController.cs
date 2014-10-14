using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public OrderController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Basket(Basket basket)
        {
            if (basket.CurrentRestaurant != Guid.Empty)
                basket.DeliveryPrice = _domain.Restaurant.GetDeliveryPrice(basket.CurrentRestaurant, basket.Total);
            return View(basket);
        }

        [HttpPost]
        public ActionResult Shipping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(Basket basket)
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}