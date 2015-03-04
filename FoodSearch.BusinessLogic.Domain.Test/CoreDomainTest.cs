
using System;
using System.IO;
using System.Linq;
using System.Reflection;

using FoodSearch.BusinessLogic.Domain.Core;
using FoodSearch.Data.Mapping.Entities;

using NUnit.Framework;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "CoreDomain intergrations tests")]
    public class CoreDomainTest : DomainTest<CoreDomain>
    {
        [TestCase(TestName = "Get all cities")]
        public void GetCitites()
        {
            var cities = Domain.GetCities();

            Assert.IsNotNull(cities);
            Assert.AreEqual(1, cities.Count());
        }

        [TestCase(TestName = "Get districts for Warsaw")]
        public void GetDistricts()
        {
            var districts = Domain.GetDistricts(1);

            Assert.AreEqual(18, districts.Count());
        }

        [TestCase("Lewartowskiego", 1, TestName = "Seach strets containing name 'Lewartowskiego'")]
        [TestCase("Nisk", 2, TestName = "Seach strets containing name 'Nisk'")]
        [TestCase("Jana", 72, TestName = "Seach strets containing name 'Jana'")]
        public void GetStreets(string query, int expectedResult)
        {
            var streets = Domain.GetStreets(1, query);

            Assert.AreEqual(expectedResult, streets.Count());
        }

        [TestCase(119, 15, TestName = "List buiding numbers for Warsaw, Lewartowskiego street")]
        [TestCase(153, 30, TestName = "List buiding numbers for Warsaw, Niska street")]
        public void GetStreetNumbers(int streetId, int expectedResult)
        {
            var streetNumbers = Domain.GetStreetNumbers(streetId);

            Assert.AreEqual(expectedResult, streetNumbers.Count());
        }

        [TestCase(TestName = "Get image")]
        public void GetImage()
        {
            var image = Domain.GetImage(1);
            Assert.IsTrue(image.ImageData.Count() > 0);
        }

        public void AddImage()
        {
            string codeBase = Assembly.GetAssembly(typeof(CoreDomainTest)).CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            var bytes = File.ReadAllBytes(Path.Combine(path, "pizza.png"));

            int imageId = Domain.AddImage(bytes, "image/png");
            Assert.IsTrue(imageId > 0);

            using (var rep = RepositoryOf<Image>())
            {
                var image = rep.Get(imageId);
                Assert.AreEqual(bytes.Length, image.ImageData.Length);
            }

        }
    }
}
