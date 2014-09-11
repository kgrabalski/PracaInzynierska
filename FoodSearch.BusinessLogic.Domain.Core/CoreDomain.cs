using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.BusinessLogic.Domain.Core.Mapping;
using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;

using City = FoodSearch.Data.Mapping.Entities.City;
using CityDto = FoodSearch.BusinessLogic.Domain.Core.Models.City;
using District = FoodSearch.Data.Mapping.Entities.District;
using DistrictDto = FoodSearch.BusinessLogic.Domain.Core.Models.District;
using Image = FoodSearch.Data.Mapping.Entities.Image;
using ImageDto = FoodSearch.BusinessLogic.Domain.Core.Models.Image;
using RestaurantInfoDto = FoodSearch.BusinessLogic.Domain.Core.Models.RestaurantInfo;
using Street = FoodSearch.Data.Mapping.Entities.Street;
using StreetDto = FoodSearch.BusinessLogic.Domain.Core.Models.Street;

namespace FoodSearch.BusinessLogic.Domain.Core
{
    public class CoreDomain : ICoreDomain
    {
        private readonly IRepositoryProvider _provider;

        public CoreDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<CityDto> GetCities()
        {
            using (var rep = _provider.GetRepository<City>())
            {
                return rep.GetAll().List().Map<IEnumerable<CityDto>>();
            }
        } 

        public IEnumerable<DistrictDto> GetDistricts(int cityId)
        {
            using (var rep = _provider.GetRepository<District>())
            {
                return rep.GetAll()
                    .Where(x => x.CityId == cityId)
                    .List()
                    .Map<IEnumerable<DistrictDto>>();
            }
        }

        public IEnumerable<StreetDto> GetStreets(int cityId, string query)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetStreets(cityId, query).Map<IEnumerable<StreetDto>>();
            }
        }

        public IEnumerable<StreetDto> GetStreets(int districtId)
        {
            using (var repA = _provider.GetRepository<Address>())
            {
                return repA.GetAll()
                    .Where(x => x.DistrictId == districtId)
                    .Select(x => x.Street)
                    .List<Street>()
                    .Map<IEnumerable<StreetDto>>();
            }
        }

        public IEnumerable<StreetNumber> GetStreetNumbers(int streetId)
        {
            using (var rep = _provider.GetRepository<Address>())
            {
                StreetNumber sn = null;
                return rep.GetAll()
                    .Where(x => x.StreetId == streetId)
                    .SelectList(list => list
                        .Select(x => x.AddressId).WithAlias(() => sn.Id)
                        .Select(x => x.Number).WithAlias(() => sn.Number))
                    .TransformUsing(Transformers.AliasToBean<StreetNumber>())
                    .List<StreetNumber>();
            }
        }

        public ImageDto GetImage(int imageId)
        {
            using (var rep = _provider.GetRepository<Image>())
            {
                return rep.Get(imageId).Map<ImageDto>();
            }
        }

        public int AddImage(byte[] imageBytes, string contentType)
        {
            using (var rep = _provider.GetRepository<Image>())
            {
                return rep.Create<int>(new Image()
                {
                    ImageData = imageBytes,
                    ContentType = contentType
                });
            }
        }

        public IEnumerable<RestaurantInfoDto> GetRestaurants(int addressId, DateTime date)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetRestaurants(addressId, date).Map<IEnumerable<RestaurantInfoDto>>();
            }
        }
    }
}
