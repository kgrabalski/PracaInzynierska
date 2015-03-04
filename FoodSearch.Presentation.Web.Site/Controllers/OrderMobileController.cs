using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.WebSocket;
using Microsoft.AspNet.SignalR;
using System;
using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class OrderMobileController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public OrderMobileController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Success(Guid paymentId)
        {
            GlobalHost.ConnectionManager.GetHubContext<FoodSearchMobileHub>()
                .Clients.Group(paymentId.ToString()).UpdatePaymentStatus(true);
            return RedirectToAction("Success", "Order");
        }

        public ActionResult Cancel(Guid paymentId)
        {
            GlobalHost.ConnectionManager.GetHubContext<FoodSearchMobileHub>()
                .Clients.Group(paymentId.ToString()).UpdatePaymentStatus(false);

            _domain.Order.UpdatePayment(paymentId, PaymentStates.Cancelled);
            Guid orderId = _domain.Order.GetOrderForPayment(paymentId).OrderId;
            _domain.Order.ChangeOrderState(orderId, OrderStates.Cancelled);
            return RedirectToAction("Cancel", "Order", new {paymentId});
        }
    }
}