using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class LogoController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public LogoController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        //get logo
        [HttpGet]
        public HttpResponseMessage Get([FromUri] int id)
        {
            var logo = _domain.Core.GetImage(id);
            if (logo != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new MemoryStream(logo.ImageData));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(logo.ContentType);
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}