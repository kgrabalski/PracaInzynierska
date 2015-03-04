using FoodSearch.Presentation.Web.Site.Models;
using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Providers
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