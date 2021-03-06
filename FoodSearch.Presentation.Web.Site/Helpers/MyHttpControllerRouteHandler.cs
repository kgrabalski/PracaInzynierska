﻿using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;

namespace FoodSearch.Presentation.Web.Site.Helpers
{
    public class MyHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyHttpControllerHandler(requestContext.RouteData);
        }
    }
}