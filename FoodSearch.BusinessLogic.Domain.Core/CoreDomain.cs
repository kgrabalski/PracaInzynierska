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
        private readonly IRepositoryHelper _helper;

        public CoreDomain(IRepositoryHelper helper)
        {
            _helper = helper;
        }

        public IEnumerable<District> GetDistricts()
        {
            using (var rep = _helper.DistrictRepository)
            {
                return rep.GetAll().List();
            }
        }

        public IEnumerable<Street> GetStreets(string query)
        {
            using (var rep = _helper.StreetRepository)
            {
                return rep.GetAll()
                    .WhereRestrictionOn(x => x.Name)
                    .IsLike(query, MatchMode.Anywhere)
                    .List();
            }
        }

        public IEnumerable<Street> GetStreets(int districtId)
        {
            using (var repS = _helper.StreetRepository)
            using (var repA = _helper.AddressRepository)
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
            using (var rep = _helper.AddressRepository)
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
    }
}
