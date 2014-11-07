
using System.Web.Http;
using System.Web.Mvc;

using FoodSearch.Service.Api.Helpers;

namespace FoodSearch.Service.Api.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                name: "User_api",
                routeTemplate: "User/{controller}/{id}",
                defaults: new { area = "User", id = RouteParameter.Optional }
                ).RouteHandler = new MyApiControllerRouteHandler();
        }
    }
}