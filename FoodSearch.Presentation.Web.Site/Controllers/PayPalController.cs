using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.WebSocket;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class PayPalController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public PayPalController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [AllowAnonymous]
        public ActionResult Ipn()
        {
            Dictionary<string, string> formVals = new Dictionary<string, string>();
            formVals.Add("cmd", "_notify-validate");

            Guid paymentId = Guid.Empty;
            Guid.TryParse(Request["item_number"], out paymentId);

            string response = GetPayPalResponse(formVals, true);

            if (response == "VERIFIED")
            {
                _domain.Order.UpdatePayment(paymentId, PaymentStates.Completed);
                var order = _domain.Order.GetOrderForPayment(paymentId);
                _domain.Order.ChangeOrderState(order.OrderId, OrderStates.Paid);

                GlobalHost.ConnectionManager.GetHubContext<RestaurantAdminHub>()
                .Clients.Group(order.RestaurantId.ToString())
                .newOrder();
            }

            return new EmptyResult();
        }

        private string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {
            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (var key in formVals)
            {
                sb.AppendFormat("&{0}={1}", key.Key, key.Value);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
    }
}