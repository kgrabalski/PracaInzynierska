using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class RatingController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RatingController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public RestaurantRating GetRestaurantRating([ModelBinder] RestaurantUser ru)
        {
            return _domain.Restaurant.GetRestaurantRating(ru.RestaurantId);
        }
    }
}
