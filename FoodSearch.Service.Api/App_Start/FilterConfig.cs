using System.Web.Mvc;

using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Service.Api
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
