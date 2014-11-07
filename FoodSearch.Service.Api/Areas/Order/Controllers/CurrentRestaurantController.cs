using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.Service.Api.Areas.Order.Models;

namespace FoodSearch.Service.Api.Areas.Order.Controllers
{
    public class CurrentRestaurantController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SetCurrentRestaurant([ModelBinder] Basket basket, [FromUri] Guid id)
        {
            basket.CurrentRestaurant = id;
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
