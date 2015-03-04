using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using System.Web.Http;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class OrderHistoryController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OrderHistoryController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }
    }
}
