using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using FoodSearch.Service.FoodSearchService;

using Ninject.Extensions.Wcf;

namespace FoodSearch.Presentation.Web.Site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new ServiceRoute("FoodSearchService", new WebServiceHostFactory(), typeof(FoodSearchService)));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new []{"FoodSearch.Presentation.Web.Site.Controllers"}
            );
        }
    }
}
