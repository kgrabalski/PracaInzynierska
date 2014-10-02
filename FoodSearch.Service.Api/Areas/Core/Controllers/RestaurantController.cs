﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;

namespace FoodSearch.Service.Api.Areas.Core.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<RestaurantInfo> GetRestaurants([FromUri] int addressId)
        {
            return _domain.Restaurant.GetRestaurants(addressId, DateTime.Now);
        }
    }
}
