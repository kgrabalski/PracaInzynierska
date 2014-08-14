using System.Web.Http;
using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin
{
    public class RestaurantAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RestaurantAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                name: "RestaurantAdmin_api",
                routeTemplate: "RestaurantAdmin/api/{controller}/{id}",
                defaults: new { area = "RestaurantAdmin", id = RouteParameter.Optional }
            );

            context.MapRoute(
                "RestaurantAdmin_default",
                "RestaurantAdmin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers" }
            );
        }
    }
}