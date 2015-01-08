using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Service.Api.Models;

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
        public IEnumerable<DeliveryAddress> GetDeliveryAddresses([ModelBinder] UserInfo ui)
        {
            return _domain.Order.GetUserDeliveryAddresses(ui.UserId);
        }
    }
}
