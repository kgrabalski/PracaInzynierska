using FoodSearch.BusinessLogic.Domain.Core.Models;
using System;
using System.Collections.Generic;
using District = FoodSearch.BusinessLogic.Domain.Core.Models.District;
using Image = FoodSearch.BusinessLogic.Domain.Core.Models.Image;
using RestaurantInfo = FoodSearch.BusinessLogic.Domain.Core.Models.RestaurantInfo;
using Street = FoodSearch.BusinessLogic.Domain.Core.Models.Street;

namespace FoodSearch.BusinessLogic.Domain.Core.Interface
{
    public interface ICoreDomain
    {
        IEnumerable<District> GetDistricts();
        IEnumerable<Street> GetStreets(string query);
        IEnumerable<Street> GetStreets(int districtId);
        IEnumerable<StreetNumber> GetStreetNumbers(int streetId);
        Image GetImage(int imageId);
        int AddImage(byte[] imageBytes, string contentType);
        IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date);
    }
}
