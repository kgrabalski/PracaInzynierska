using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Service.Api.Areas.Order.Models;
using FoodSearch.Service.Api.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Service.Api.Areas.Order.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OrderController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public HttpResponseMessage GetOrderState([FromUri] Guid id)
        {
            var status = _domain.Order.GetDeliveryStatus(id);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        [HttpPost]
        public HttpResponseMessage CreateOrder([ModelBinder] Basket basket, [ModelBinder] UserInfo ui, OrderModel model)
        {
            if (ModelState.IsValid)
            {
                DeliveryTypes dt = (DeliveryTypes) model.DeliveryTypeId;
                PaymentTypes pt = (PaymentTypes) model.PaymentTypeId;
                
                var orderItems = basket.Items.Select(x => new OrderItem() {DishId = x.DishId, Quantity = x.Count}).ToList();

                int? deliveryAddressId = null;
                if (dt == DeliveryTypes.Shipping)
                {
                    deliveryAddressId = _domain.User.CreateDeliveryAddress(ui.UserId, model.AddressId, model.FlatNumber);
                }

                var orderInfo = _domain.Order.CreateOrder(ui.UserId, basket.CurrentRestaurant, orderItems, dt, pt, deliveryAddressId);

                if (pt == PaymentTypes.Cash)
                {
                    _domain.Order.ChangeOrderState(orderInfo.OrderId, OrderStates.Paid);
                }

                return Request.CreateResponse(HttpStatusCode.Created, orderInfo);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
