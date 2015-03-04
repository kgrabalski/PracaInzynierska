using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;

namespace FoodSearch.Service.Api.Helpers
{
    public class MyApiControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyApiControllerHandler(requestContext.RouteData);
        }
    }
}
