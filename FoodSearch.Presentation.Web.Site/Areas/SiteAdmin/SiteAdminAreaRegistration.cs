using System.Web.Mvc;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin
{
    public class SiteAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SiteAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SiteAdmin_default",
                "SiteAdmin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers" }
            );
        }
    }
}