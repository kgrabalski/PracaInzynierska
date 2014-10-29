using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Models;

namespace FoodSearch.Service.Api.Areas.Order.Controllers
{
    public class PaymentTypeController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public PaymentTypeController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public IEnumerable<PaymentType> GetPaymentTypes()
        {
            return _domain.Order.GetPaymentTypes();
        }
    }
}
