using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.BusinessLogic.Domain.FoodSearch;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Service.Contracts.Response;

using Street = FoodSearch.Service.Contracts.Response.Street;

namespace FoodSearch.Service.FoodSearchService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FoodSearchService : IFoodSearchService
    {
        private readonly IFoodSearchDomain _domain;

        public FoodSearchService(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public FoodSearchService() : this(new FoodSearchDomain())
        {
            
        }

        public IEnumerable<Street> Streets(string query)
        {
            return _domain.Core.GetStreets(query).Select(x => new Street()
            {
                StreetId = x.StreetId,
                Name = x.Name
            });
        }

        public IEnumerable<Contracts.Response.StreetNumber> StreetNumbers(string streetId)
        {
            int strId;
            if (!int.TryParse(streetId, out strId)) return new List<Contracts.Response.StreetNumber>();
            return _domain.Core.GetStreetNumbers(strId).Select(x => new Contracts.Response.StreetNumber()
            {
                AddressId = x.AddressId,
                Number = x.Number
            });
        }

        public IEnumerable<RestaurantInfo> Restaurants(string addressId)
        {
            int adrId;
            if (!int.TryParse(addressId, out adrId)) return new List<RestaurantInfo>();
            return _domain.Core.GetRestaurants(adrId, DateTime.Now).Select(x => new RestaurantInfo()
            {
                RestaurantId = x.RestaurantId,
                RestaurantName = x.RestaurantName,
                LogoId = x.LogoId,
                TimeFrom = x.TimeFrom,
                TimeTo = x.TimeTo
            });
        }
    }
}
