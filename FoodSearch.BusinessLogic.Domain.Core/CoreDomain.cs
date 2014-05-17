using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using NHibernate.Criterion;
using NHibernate.Transform;

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
            using (var rep = _provider.GetRepository<District>())
            {
                return rep.GetAll().List();
            }
        }

        public IEnumerable<Street> GetStreets(string query)
        {
            using (var rep = _provider.GetRepository<Street>())
            {
                return rep.GetAll()
                    .WhereRestrictionOn(x => x.Name)
                    .IsInsensitiveLike(query, MatchMode.Anywhere)
                    .List()
                    .ToList();
            }
        }

        public IEnumerable<Street> GetStreets(int districtId)
        {
            using (var repS = _provider.GetRepository<Street>())
            using (var repA = _provider.GetRepository<Address>())
            {
                return repA.GetAll()
                    .Where(x => x.DistrictId == districtId)
                    .Select(x => x.Street)
                    .List<Street>()
                    .ToList();
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

        public Address GetAddress(int addressId)
        {
            using (var rep = _provider.GetRepository<Address>())
            {
                return rep.Get(addressId);
            }
        }

        public Image GetImage(int imageId)
        {
            using (var rep = _provider.GetRepository<Image>())
            {
                return rep.Get(imageId);
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
    }
}
