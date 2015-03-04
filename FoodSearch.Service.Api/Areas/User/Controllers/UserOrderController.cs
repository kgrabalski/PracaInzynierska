using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.Service.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Service.Api.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class UserOrderController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public UserOrderController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<UserOrder> GetUserOrders([ModelBinder] UserInfo ui, [FromUri] int page, [FromUri] int pageSize)
        {
            return _domain.User.GetUserOrders(ui.UserId, page, pageSize);
        }

        [HttpGet]
        public HttpResponseMessage GetUserOrder([FromUri] Guid id)
        {
            var items = _domain.User.GetUserOrderItems(id);
            if (!items.Any()) return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
    }
}
