using FoodSearch.Presentation.Web.Site.Helpers;
using System.Web.Mvc;

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
