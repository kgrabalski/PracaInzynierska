using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class NewOrderController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public NewOrderController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<RestaurantOrder> GetOrders([ModelBinder] RestaurantUser ru)
        {
            var orders = _domain.RestaurantAdmin.GetRestaurantOrders(ru.RestaurantId, true);
            return orders;
        }

        [HttpDelete]
        public HttpResponseMessage CancelOrder([FromUri] Guid orderId, [FromUri] string cancellationReason)
        {
            if (string.IsNullOrEmpty(cancellationReason))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            bool result = _domain.Order.CancelOrder(orderId, cancellationReason);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }

        [HttpPost]
        [ValidateModel]
        public HttpResponseMessage ConfirmOrder([FromUri] Guid id, ConfirmOrderModel model)
        {
            TimeSpan dt = TimeSpan.ParseExact(model.DeliveryTime, "g", CultureInfo.InvariantCulture);
            bool result = _domain.Order.ConfirmOrder(id, dt);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}
