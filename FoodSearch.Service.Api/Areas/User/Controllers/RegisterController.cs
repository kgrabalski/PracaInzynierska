using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

namespace FoodSearch.Service.Api.Areas.User.Controllers
{
    public class RegisterController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RegisterController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }
    }
}
