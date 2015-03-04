using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class AddressController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public AddressController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public HttpResponseMessage GetAddress([FromUri] int id)
        {
            var address = _domain.SiteAdmin.GetAddress(id);
            return Request.CreateResponse(address != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, address);
        }
    }
}
