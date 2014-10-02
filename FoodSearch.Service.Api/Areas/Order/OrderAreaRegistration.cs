using System.Web.Http;
using System.Web.Mvc;

namespace FoodSearch.Service.Api.Areas.Order
{
    public class OrderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Order";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "Order_api",
                routeTemplate: "Order/{controller}/{id}",
                defaults: new {area = "Order", id = RouteParameter.Optional}
                );
        }
    }
}