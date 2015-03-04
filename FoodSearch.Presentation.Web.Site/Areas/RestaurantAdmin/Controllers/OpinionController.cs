using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class OpinionController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OpinionController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        [ValidateModel]
        public HttpResponseMessage GetOpinions([ModelBinder] RestaurantUser ru, [FromUri] OpinionFilterModel filter)
        {
            var opinions = _domain.Restaurant.GetOpinions(ru.RestaurantId, filter.Rating, filter.Page);
            return Request.CreateResponse(HttpStatusCode.OK, opinions);
        } 
    }
}
