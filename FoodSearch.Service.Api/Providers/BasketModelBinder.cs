using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;

using FoodSearch.Service.Api.Areas.Order.Models;

namespace FoodSearch.Service.Api.Providers
{
    public class BasketModelBinder : IModelBinder
    {
        private readonly string _sessionKey = "Basket";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Basket basket = controllerContext.HttpContext.Session[_sessionKey] as Basket;
            if (basket == null)
            {
                basket = new Basket();
                controllerContext.HttpContext.Session[_sessionKey] = basket;
            }
            return basket;
        }
    }
}
