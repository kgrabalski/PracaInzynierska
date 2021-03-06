﻿using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FoodSearch.Service.Api.Areas.Core.Controllers
{
    public class LogoController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public LogoController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

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
