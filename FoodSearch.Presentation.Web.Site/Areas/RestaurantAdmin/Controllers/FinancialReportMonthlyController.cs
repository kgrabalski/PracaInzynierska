using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    public class FinancialReportMonthlyController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public FinancialReportMonthlyController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        [ValidateModel]
        public HttpResponseMessage GetRestaurantMonthlyFinancialReport([ModelBinder] RestaurantUser ru, [ModelBinder] ReportRangeModel model)
        {
            DateTime df, dt;
            bool canParseDateFrom = DateTime.TryParseExact(model.DateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out df);
            bool canParseDateTo = DateTime.TryParseExact(model.DateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            if (!canParseDateFrom || !canParseDateTo) return Request.CreateResponse(HttpStatusCode.BadRequest);
            if (dt < df) return Request.CreateResponse(HttpStatusCode.BadRequest);

            var report = _domain.RestaurantAdmin.GetRestaurantMonthlyFinancialReports(ru.RestaurantId, df, dt, true);
            return Request.CreateResponse(HttpStatusCode.OK, report);
        }
    }
}
