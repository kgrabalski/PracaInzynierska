using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

using FoodSearch.Service.Api.Areas.Order.Models;

namespace FoodSearch.Service.Api.Providers
{
    public class BasketModelBinderWebApi : IModelBinder
    {
        private const string SessionKey = "Basket";

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            Basket basket = HttpContext.Current.Session[SessionKey] as Basket;
            if (basket == null)
            {
                basket = new Basket();
                HttpContext.Current.Session[SessionKey] = basket;
            }
            bindingContext.Model = basket;
            return true;
        }
    }
}
