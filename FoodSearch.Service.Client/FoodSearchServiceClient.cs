using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Service.Contracts;
using FoodSearch.Service.Contracts.Response;

namespace FoodSearch.Service.Client
{
    public class FoodSearchServiceClient : RestServiceClientBase
    {
        public FoodSearchServiceClient()
        {
            apiPath += "FoodSearchService/";
        }

        public Task<List<Street>> GetStreets(string query)
        {
            return RestGet<List<Street>>("Streets/" + query);
        }

        public Task<List<StreetNumber>> GetStreetNumbers(int streetId)
        {
            return RestGet<List<StreetNumber>>("StreetNumbers/" + streetId);
        }
    }
}
