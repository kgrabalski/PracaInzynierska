using System;
using System.Linq;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;
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
                basket.DeliveryPrice = _domain.Order.GetDeliveryPrice(basket.CurrentRestaurant, basket.Total);
            return View(basket);
        }

        [HttpPost]
        public ActionResult Shipping(Basket basket, UserInfo ui)
        {
            var shippingModel = new ShippingModel()
            {
                DeliveryAddress = _domain.Order.GetUserDeliveryAddress(ui.UserId),
                DeliveryTypes = _domain.Order.GetDeliveryTypes(),
                PaymentTypes = _domain.Order.GetPaymentTypes(),
                RestaurantId = basket.CurrentRestaurant
            };
            return View(shippingModel);
        }

        [HttpPost]
        public ActionResult CreateOrder(Basket basket, int deliveryType, int paymentType, UserInfo ui)
        {
            DeliveryTypes dt = (DeliveryTypes) deliveryType;
            PaymentTypes pt = (PaymentTypes) paymentType;
            var orderItems = basket.Items.Select(x => new OrderItem() { DishId = x.DishId, Quantity = x.Count }).ToList();
            var orderInfo = _domain.Order.CreateOrder(ui.UserId, basket.CurrentRestaurant, orderItems, dt, pt);
            if (pt == PaymentTypes.Cash)
            {
                _domain.Order.ChangeOrderState(orderInfo.OrderId, OrderStates.Paid);
                return RedirectToAction("Success");
            }

            string ipn = "http://foodsearch.azurewebsites.net/PayPal/Ipn";
            string success = "http://foodsearch.azurewebsites.net/Order/Success";
            string cancel = "http://foodsearch.azurewebsites.net/Order/Cancel?paymentId=" + orderInfo.PaymentId;
            string url = string.Format("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=kgrabalski@poczta.onet.pl&item_number={0}&item_name={1}&rm=2&LC=PL&country=PL&amount={2}&currency_code=PLN&notify_url={3}&return={4}&cancel_return={5}",
                            orderInfo.PaymentId,
                            "Restaurant " + orderInfo.RestaurantName,
                            orderInfo.Price,
                            ipn,
                            success,
                            cancel);

            return Redirect(url);
        }

        [AllowAnonymous]
        public ActionResult Success()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Cancel(Guid paymentId)
        {
            _domain.Order.UpdatePayment(paymentId, PaymentStates.Cancelled);
            Guid orderId = _domain.Order.GetOrderForPayment(paymentId);
            _domain.Order.ChangeOrderState(orderId, OrderStates.Cancelled);
            return View();
        }
    }
}