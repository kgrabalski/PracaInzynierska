using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace FoodSearch.Service.Api.Helpers
{
    public class MyApiControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public MyApiControllerHandler(RouteData routeData) : base(routeData)
        {
            
        }
    }
}
