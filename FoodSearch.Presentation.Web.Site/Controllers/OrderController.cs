using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
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
            var deliveryAddress = _domain.Order.GetUserDeliveryAddress(ui.UserId);
            return View(deliveryAddress);
        }

        [HttpPost]
        public ActionResult CreateOrder(Basket basket, int deliveryType, int paymentType, UserInfo ui)
        {
            DeliveryTypes dt = (DeliveryTypes) deliveryType;
            PaymentTypes pt = (PaymentTypes) paymentType;
            var orderItems = basket.Items.Select(x => new OrderItem() { DishId = x.DishId, Quantity = x.Count }).ToList();
            var orderInfo = _domain.Order.CreateOrder(ui.UserId, basket.CurrentRestaurant, orderItems, dt, pt);
            if (pt == PaymentTypes.Cash) return RedirectToAction("Success");

            string ipn = "http://foodsearch.azurewebsites.net/Order/UserPaymentIpn";
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

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Cancel(Guid paymentId)
        {
            _domain.Order.UpdatePayment(paymentId, PaymentStates.Cancelled);
            return View();
        }

        [AllowAnonymous]
        public ActionResult UserPaymentIpn()
        {
            Dictionary<string, string> formVals = new Dictionary<string, string>();
            formVals.Add("cmd", "_notify-validate");

            Guid paymentId = Guid.Empty;
            Guid.TryParse(Request["item_number"], out paymentId);

            string response = GetPayPalResponse(formVals, true);

            _domain.Order.LogPaypalResponse(paymentId, response);

            if (response == "VERIFIED")
            {
                _domain.Order.UpdatePayment(paymentId, PaymentStates.Completed);
            }

            return new EmptyResult();
        }

        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
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