using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.RestraurantAdmin;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestClass]
    public class RestaurantAdminDomainTest
    {
        private ISessionSource sessionSource;
        private IRepositoryProvider provider;
        private IRestaurantAdminDomain domain;

        [TestInitialize]
        public void BusinessLogic_Domain_RestaurantAdmin_InitializeTest()
        {
            sessionSource = new SessionSource(Databases.FoodSearchTest);
            provider = new FoodSearchRepositoryProvider(sessionSource);
            domain = new RestaurantAdminDomain(provider);
        }
    }
}
