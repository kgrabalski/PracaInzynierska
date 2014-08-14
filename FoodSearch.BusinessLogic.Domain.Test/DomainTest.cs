using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;
using FoodSearch.Data.Mapping.Test;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    public abstract class DomainTest<TDomain> : DbTest where TDomain : class
    {
        private readonly TDomain _domain;
        protected TDomain Domain { get { return _domain; } }

        private readonly IRepositoryProvider _provider;
        protected IRepositoryProvider RepositoryProvider
        {
            get { return _provider;}
        }

        protected DomainTest()
        {
            _provider = new FoodSearchRepositoryProvider(SessionSource);
            _domain = (TDomain) Activator.CreateInstance(typeof (TDomain), RepositoryProvider);
        }
    } 
}
