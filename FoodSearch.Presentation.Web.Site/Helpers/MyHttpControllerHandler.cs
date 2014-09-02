using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace FoodSearch.Presentation.Web.Site.Helpers
{
    public class MyHttpControllerHandler : HttpControllerHandler, IReadOnlySessionState
    {
        public MyHttpControllerHandler(RouteData routeData)
            : base(routeData)
        {
        }
    }
}