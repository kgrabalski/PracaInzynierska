using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.Data.Mapping.Entities;

namespace FoodSearch.BusinessLogic.Domain.Core.Interface
{
    public interface ICoreDomain
    {
        IEnumerable<District> GetDistricts();
        IEnumerable<Street> GetStreets(string query);
        IEnumerable<Street> GetStreets(int districtId);
        IEnumerable<StreetNumber> GetStreetNumbers(int streetId);
        Address GetAddress(int addressId);
        byte[] GetImage(int imageId);
    }
}
