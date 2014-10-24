using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

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

        [System.Web.Http.HttpGet]
        [OutputCache(NoStore = true)]
        public IEnumerable<BasketItem> GetBasket([System.Web.Http.ModelBinding.ModelBinder] Basket basket)
        {
            return basket.Items;
        }
        
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Add([System.Web.Http.ModelBinding.ModelBinder] Basket basket, [FromUri] int id)
        {
            var dish = _domain.Restaurant.GetDish(id);
            if (dish == null) return Request.CreateResponse(HttpStatusCode.NotFound);
            basket.AddItem(dish);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete([System.Web.Http.ModelBinding.ModelBinder] Basket basket, [FromUri] int id)
        {
            bool removed = basket.RemoveItem(id);
            return Request.CreateResponse(removed ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage DeleteAll([System.Web.Http.ModelBinding.ModelBinder] Basket basket)
        {
            basket.ClearBasket();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }

}
