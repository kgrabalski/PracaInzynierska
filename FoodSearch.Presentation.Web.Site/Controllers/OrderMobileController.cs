using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.WebSocket;

using Microsoft.AspNet.SignalR;

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
            return RedirectToAction("Cancel", "Order", new {paymentId});
        }
    }
}