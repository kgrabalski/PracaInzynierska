﻿using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Service.Api.Areas.Order.Models;
using FoodSearch.Service.Api.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Service.Api.Areas.Order.Controllers
{
    [Authorize(Roles = "User")]
    public class DeliveryAddressController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public DeliveryAddressController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public HttpResponseMessage GetDeliveryAddresses([ModelBinder] UserInfo ui, [ModelBinder] Basket basket)
        {
            var deliveryAddress = _domain.User.GetDeliveryAddress(ui.UserId, basket.AddressId);
            return Request.CreateResponse(deliveryAddress != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, deliveryAddress);
        }
    }
}
