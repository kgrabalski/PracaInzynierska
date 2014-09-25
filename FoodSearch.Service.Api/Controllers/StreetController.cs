using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

namespace FoodSearch.Service.Api.Controllers
{
    public class StreetController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public StreetController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<Street> GetStreets(int cityId, string query)
        {
            return _domain.Core.GetStreets(cityId, query);
        }

        [HttpGet]
        public IEnumerable<StreetNumber> GetStreetNumbers([FromUri] int id)
        {
            return _domain.Core.GetStreetNumbers(id);
        }
    }
}
