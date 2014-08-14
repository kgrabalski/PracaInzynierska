using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Ninject.Web.WebApi;

namespace FoodSearch.Presentation.Web.Site.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.DependencyResolver = new NinjectDependencyResolver(MvcApplication.DependencyResolver);
        }
    }
}