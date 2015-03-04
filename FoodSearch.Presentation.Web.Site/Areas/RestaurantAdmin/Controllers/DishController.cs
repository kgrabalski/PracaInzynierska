using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class DishController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public DishController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<Dish> GetAll([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetDishes(ru.RestaurantId);
        } 

        [HttpPost]
        public async Task<HttpResponseMessage> CreateDish([ModelBinder] RestaurantUser ru)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }
            var request = HttpContext.Current.Request;
            if (request.Files.Count == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var file = request.Files[0];
            byte[] imageBytes = new byte[file.ContentLength];
            await file.InputStream.ReadAsync(imageBytes, 0, file.ContentLength);

            var form = request.Form;
            string dishName = form["DishName"];
            int dishGroupId = int.Parse(form["DishGroupId"]);
            decimal price = decimal.Parse(form["Price"], CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(dishName) || dishGroupId <= 0 || price <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            int imageId = _domain.Core.AddImage(imageBytes, file.ContentType);
            var result = _domain.RestaurantAdmin.CreateDish(ru.RestaurantId, dishName, dishGroupId, price, imageId);
            return Request.CreateResponse(result != null ? HttpStatusCode.Created : HttpStatusCode.Conflict, result);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteDish([ModelBinder] RestaurantUser ru, [FromUri] int id)
        {
            bool result = _domain.RestaurantAdmin.DeleteDish(ru.RestaurantId, id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }

        [HttpPut]
        [ValidateModel]
        public HttpResponseMessage EditDish([ModelBinder] RestaurantUser ru, [FromUri] int id, DishModel model)
        {
            var result = _domain.RestaurantAdmin.EditDish(ru.RestaurantId, id, model.DishName, model.DishGroupId, model.Price);
            return Request.CreateResponse(result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}