using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.SiteAdmin;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestClass]
    public class SiteAdminDomainTest
    {
        private ISessionSource sessionSource;
        private IRepositoryProvider provider;
        private ISiteAdminDomain domain;

        [TestInitialize]
        public void BusinessLogic_Domain_SiteAdmin_InitializeTest()
        {
            sessionSource = new SessionSource(Databases.FoodSearchTest);
            provider = new FoodSearchRepositoryProvider(sessionSource);
            domain = new SiteAdminDomain(provider);
        }

        [TestMethod]
        public void BusinessLogic_Domain_SiteAdmin_CreateRestaurant()
        {
            var id = domain.CreateRestaurant("Testowa resrauracja", 1315, 2);
            Assert.IsTrue(id != Guid.Empty);
        }

        [TestMethod]
        public void BusinessLogic_Domain_SiteAdmin_GetRestaurants()
        {
            var rest = domain.GetRestaurants();
        }
    }
}
