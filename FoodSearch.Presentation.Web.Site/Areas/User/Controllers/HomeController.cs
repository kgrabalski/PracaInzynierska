using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Areas.User.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public HomeController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index(UserInfo ui)
        {
            var model = new UserPanelModel()
            {
                UserDetails = _domain.User.GetUserDetails(ui.UserId),
                DeliveryAddresses = _domain.User.GetUserDeliveryAddresses(ui.UserId)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult GetUserOrders(UserInfo ui, int page = 0, int pageSize = 10)
        {
            var orders = _domain.User.GetUserOrders(ui.UserId, page, pageSize);
            return Json(orders, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult GetUserOrderItems(Guid orderId)
        {
            var orderItems = _domain.User.GetUserOrderItems(orderId);
            return Json(orderItems, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult CreateUserDeliveryAddress(UserInfo ui, int addressId, string flatNumber)
        {
            int deliveryAddressId = _domain.User.CreateDeliveryAddress(ui.UserId, addressId, flatNumber);
            var deliveryAddress = _domain.User.GetUserDeliveryAddress(ui.UserId, deliveryAddressId);
            return Json(deliveryAddress, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult DeleteUserDeliveryAddress(int deliveryAddressId)
        {
            _domain.User.DeleteDeliveryAddress(deliveryAddressId);
            return Json(true, JsonRequestBehavior.DenyGet);
        }
    }
}