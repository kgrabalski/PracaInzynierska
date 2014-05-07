using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Core;
using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestClass]
    public class CoreDomainTest
    {
        private ISessionSource sessionSource;
        private IRepositoryProvider provider;
        private ICoreDomain domain;

        [TestInitialize]
        public void BusinessLogic_Domain_Core_InitializeTest()
        {
            sessionSource = new SessionSource(Databases.FoodSearchTest);
            provider = new FoodSearchRepositoryProvider(sessionSource);
            domain = new CoreDomain(provider);
        }

        [TestMethod]
        public void BusinessLogic_Domain_Core_GetDistricts()
        {
            var dist = domain.GetDistricts();
            Assert.IsTrue(dist.Any());
        }

        [TestMethod]
        public void BusinessLogic_Domain_Core_GetStreetsFromName()
        {
            var streets = domain.GetStreets("Inf");
            Assert.AreEqual(1, streets.Count());
            Assert.AreEqual("Inflancka", streets.ElementAt(0).Name);
        }

        [TestMethod]
        public void BusinessLogic_Domain_Core_GetStreetsFromDistrict()
        {
            var streets = domain.GetStreets(1);
            Assert.IsTrue(streets.Any());
        }

        [TestMethod]
        public void BusinessLogic_Domain_Core_GetStreetNumbers()
        {
            var numbers = domain.GetStreetNumbers(78);
            Assert.IsTrue(numbers.Any());
        }

        [TestMethod]
        public void BusinessLogic_Domain_Core_GetImage()
        {
            var image = domain.GetImage(1);
            Assert.IsNotNull(image);
            Assert.IsTrue(image.Length > 0);
        }
    }
}
