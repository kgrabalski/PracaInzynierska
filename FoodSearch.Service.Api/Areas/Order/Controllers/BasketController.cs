using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Service.Api.Areas.Order.Models;

namespace FoodSearch.Service.Api.Areas.Order.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public BasketController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<BasketItem> GetBasket([ModelBinder] Basket basket)
        {
            return basket.Items;
        }
        
        [HttpPost]
        public HttpResponseMessage Add([ModelBinder] Basket basket, [FromUri] int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public HttpResponseMessage Delete([ModelBinder] Basket basket, [FromUri] int id)
        {
            bool removed = basket.RemoveItem(id);
            return Request.CreateResponse(removed ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }

}
