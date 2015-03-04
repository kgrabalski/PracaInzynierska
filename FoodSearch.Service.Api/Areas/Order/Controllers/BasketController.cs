using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Service.Api.Areas.Order.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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
            var dish = _domain.Restaurant.GetDish(id);
            if (dish == null) return Request.CreateResponse(HttpStatusCode.NotFound);
            basket.AddItem(dish);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpDelete]
        public HttpResponseMessage Delete([ModelBinder] Basket basket, [FromUri] int id)
        {
            bool removed = basket.RemoveItem(id);
            return Request.CreateResponse(removed ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAll([ModelBinder] Basket basket)
        {
            basket.ClearBasket();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }

}
