using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Service.Api.Areas.Order.Models;
using FoodSearch.Service.Api.Models;

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

        [HttpPost]
        public HttpResponseMessage CreateOrder([ModelBinder] Basket basket, [ModelBinder] UserInfo ui, OrderModel model)
        {
            DeliveryTypes dt = (DeliveryTypes) model.DeliveryTypeId;
            PaymentTypes pt = (PaymentTypes) model.PaymentTypeId;
            var orderItems = basket.Items.Select(x => new OrderItem() {DishId = x.DishId, Quantity = x.Count}).ToList();
            var orderInfo = _domain.Order.CreateOrder(ui.UserId, basket.CurrentRestaurant, orderItems, dt, pt);
            return Request.CreateResponse(HttpStatusCode.Created, orderInfo);
        }
    }
}
