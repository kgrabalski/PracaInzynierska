using FoodSearch.BusinessLogic.Domain.Core.Models;
using System.Collections.Generic;
using District = FoodSearch.BusinessLogic.Domain.Core.Models.District;
using Image = FoodSearch.BusinessLogic.Domain.Core.Models.Image;
using Street = FoodSearch.BusinessLogic.Domain.Core.Models.Street;

namespace FoodSearch.BusinessLogic.Domain.Core.Interface
{
    public interface ICoreDomain
    {
        IEnumerable<City> GetCities();
        IEnumerable<District> GetDistricts(int cityId);
        IEnumerable<Street> GetStreets(int cityId, string query);
        IEnumerable<StreetNumber> GetStreetNumbers(int streetId);
        Image GetImage(int imageId);
        int AddImage(byte[] imageBytes, string contentType);
    }
}
