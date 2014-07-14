using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using FoodSearch.Service.Contracts.Response;

using StreetNumber = FoodSearch.BusinessLogic.Domain.Core.Models.StreetNumber;

namespace FoodSearch.Service.FoodSearchService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFoodSearchService" in both code and config file together.
    [ServiceContract]
    public interface IFoodSearchService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Streets/{query}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Wyszukaj ulice po nazwie")]
        IEnumerable<Street> Streets(string query);

        [OperationContract]
        [WebGet(UriTemplate = "/StreetNumbers/{streetId}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Zwraca numery budynków dla danej ulicy")]
        IEnumerable<Contracts.Response.StreetNumber> StreetNumbers(string streetId);

        [OperationContract]
        [WebGet(UriTemplate = "/GetRestaurants/{addressId}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Zwraca listę restauracji dowożących pod podany adres")]
        IEnumerable<Contracts.Response.RestaurantInfo> Restaurants(string addressId);
    }
}
