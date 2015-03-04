using FoodSearch.Service.Api.Helpers;
using System.Web.Http;
using System.Web.Mvc;

namespace FoodSearch.Service.Api.Areas.Core
{
    public class CoreAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Core";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "Core_api",
                routeTemplate: "Core/{controller}/{id}",
                defaults: new {area = "Core", id = RouteParameter.Optional}
                ).RouteHandler = new MyApiControllerRouteHandler();
        }
    }
}