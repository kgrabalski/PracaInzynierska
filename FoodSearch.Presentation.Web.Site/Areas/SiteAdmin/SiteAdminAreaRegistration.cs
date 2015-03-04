using FoodSearch.Presentation.Web.Site.Helpers;
using System.Web.Http;
using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin
{
    public class SiteAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SiteAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                name: "SiteAdmin_api",
                routeTemplate: "SiteAdmin/api/{controller}/{id}",
                defaults: new { area = "SitetAdmin", id = RouteParameter.Optional }
            )
            .RouteHandler = new MyHttpControllerRouteHandler();


            context.MapRoute(
                "SiteAdmin_default",
                "SiteAdmin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers" }
            );
        }
    }
}