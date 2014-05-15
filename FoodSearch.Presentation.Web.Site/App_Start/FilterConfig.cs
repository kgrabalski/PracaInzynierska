using System.Web;
using System.Web.Mvc;

using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Presentation.Web.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CompressAttribute());
        }
    }
}
