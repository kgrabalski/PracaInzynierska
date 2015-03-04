using FoodSearch.Presentation.Web.Site.Helpers;
using Ninject.Web.WebApi;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

#if DEBUG
            config.Formatters.JsonFormatter.Indent = true;
#endif

            config.DependencyResolver = new NinjectDependencyResolver(MvcApplication.DependencyResolver);

            config.Services.Insert(typeof(ModelBinderProvider), 0, new RestaurantUserModelBinderProvider());
        }
    }
}