using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Models;
using FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class RestaurantController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        [ValidateModel]
        public IEnumerable<Restaurant> GetAll([ModelBinder] QueryModel model)
        {
            return _domain.SiteAdmin.GetRestaurants(model.Query, model.Page, model.PageSize);
        }

        [HttpGet]
        public HttpResponseMessage GetRestaurant([FromUri] Guid id)
        {
            var restaurant = _domain.SiteAdmin.GetRestaurant(id);
            return Request.CreateResponse(restaurant != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, restaurant);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 0) 
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var file = httpRequest.Files[0];
            byte[] logoBytes = new byte[file.ContentLength];
            await file.InputStream.ReadAsync(logoBytes, 0, file.ContentLength);

            var form = httpRequest.Form;
            string restaurantName = form["Name"];
            int addressId = int.Parse(form["AddressId"]);
            string firstName = form["FirstName"];
            string lastName = form["LastName"];
            string userPassword = form["UserPassword"];
            string userEmail = form["UserEmail"];

            int logoId = _domain.Core.AddImage(logoBytes, file.ContentType);
            Guid? restaurantId = _domain.SiteAdmin.CreateRestaurant(restaurantName, addressId, logoId, userPassword, userEmail, firstName, lastName);
            return Request.CreateResponse(restaurantId.HasValue ? HttpStatusCode.Created : HttpStatusCode.Conflict, new {Id = restaurantId});
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] Guid id)
        {
            bool result = _domain.SiteAdmin.DeleteRestaurant(id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}