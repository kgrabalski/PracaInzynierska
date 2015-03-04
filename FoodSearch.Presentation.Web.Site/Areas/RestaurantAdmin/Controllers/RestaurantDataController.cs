using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
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
    public class RestaurantDataController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantDataController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public RestaurantData GetRestaurantData([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetRestaurantData(ru.RestaurantId);
        }

        [HttpPost]
        [ValidateModel]
        public HttpResponseMessage UpdateRestaurantData([ModelBinder] RestaurantUser ru, UpdateRestaurantDataModel model)
        {
            _domain.RestaurantAdmin.UpdateRestaurantData(
                ru.RestaurantId,
                model.Name,
                model.IsOpen,
                model.MinOrderAmount,
                model.DeliveryPrice,
                model.FreeDeliveryFrom
                );
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}