﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

using FoodSearch.Service.Api.Areas.Order.Models;
using FoodSearch.Service.Api.Providers;

using Ninject.Web.WebApi;

namespace FoodSearch.Service.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            AreaRegistration.RegisterAllAreas();
            // Web API routes
            config.MapHttpAttributeRoutes();

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            config.DependencyResolver = new NinjectDependencyResolver(MvcApplication.DependencyResolver);

            config.Routes.MapHttpRoute(
                name: "Default_api",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, area = "" }
            );

            ModelBinders.Binders.Add(typeof(Basket), new BasketModelBinder());
        }
    }
}
