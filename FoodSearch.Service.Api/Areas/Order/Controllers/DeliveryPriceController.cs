using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Service.Api.Areas.Order.Models;

namespace FoodSearch.Service.Api.Areas.Order.Controllers
{
    public class DeliveryPriceController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public DeliveryPriceController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public decimal GetDeliveryPrice(DeliveryPriceModel model)
        {
            return _domain.Order.GetDeliveryPrice(model.RestaurantId, model.TotalPrice);
        }
    }
}
