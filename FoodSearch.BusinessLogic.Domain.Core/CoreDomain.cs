using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.BusinessLogic.Domain.Core.Mapping;
using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;
using FoodSearch.Data.Mapping.StoredProcedure.Results;

using NHibernate.Criterion;
using NHibernate.Transform;

using District = FoodSearch.BusinessLogic.Domain.Core.Models.District;
using DistrictEnt = FoodSearch.Data.Mapping.Entities.District;

using Image = FoodSearch.BusinessLogic.Domain.Core.Models.Image;
using ImageEnt = FoodSearch.Data.Mapping.Entities.Image;

using RestaurantInfo = FoodSearch.BusinessLogic.Domain.Core.Models.RestaurantInfo;

using Street = FoodSearch.BusinessLogic.Domain.Core.Models.Street;
using StreetEnt = FoodSearch.Data.Mapping.Entities.Street;

namespace FoodSearch.BusinessLogic.Domain.Core
{
    public class CoreDomain : ICoreDomain
    {
        private readonly IRepositoryProvider _provider;

        public CoreDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<District> GetDistricts()
        {
            using (var rep = _provider.GetRepository<DistrictEnt>())
            {
                return rep.GetAll().List().Map<IEnumerable<District>>();
            }
        }

        public IEnumerable<Street> GetStreets(string query)
        {
            using (var rep = _provider.GetRepository<StreetEnt>())
            {
                return rep.GetAll()
                    .WhereRestrictionOn(x => x.Name)
                    .IsInsensitiveLike(query, MatchMode.Anywhere)
                    .List()
                    .Map<IEnumerable<Street>>();
            }
        }

        public IEnumerable<Street> GetStreets(int districtId)
        {
            using (var repA = _provider.GetRepository<Address>())
            {
                return repA.GetAll()
                    .Where(x => x.DistrictId == districtId)
                    .Select(x => x.Street)
                    .List<StreetEnt>()
                    .Map<IEnumerable<Street>>();
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
                        .Select(x => x.AddressId).WithAlias(() => sn.AddressId)
                        .Select(x => x.Number).WithAlias(() => sn.Number))
                    .TransformUsing(Transformers.AliasToBean<StreetNumber>())
                    .List<StreetNumber>();
            }
        }

        public Image GetImage(int imageId)
        {
            using (var rep = _provider.GetRepository<ImageEnt>())
            {
                return rep.Get(imageId).Map<Image>();
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

        public IEnumerable<RestaurantInfo> GetRestaurants(int addressId, DateTime date)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetRestaurants(addressId, date).Map<IEnumerable<RestaurantInfo>>();
            }
        }
    }
}
