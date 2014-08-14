using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using FoodSearch.Presentation.Web.Site.Helpers;

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

            config.Services.Insert(typeof(ModelBinderProvider), 0, new RestaurantUserModelBinderProvider());
        }
    }
}