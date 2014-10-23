using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
