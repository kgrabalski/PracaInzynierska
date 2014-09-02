using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Helpers
{
    public class AreaAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                var area = filterContext.RouteData.DataTokens["area"];
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary 
                { 
                    { "area", area},
                    { "controller", "Account" }, 
                    { "action", "Login" }, 
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl } 
                });
            }
        }
    }
}