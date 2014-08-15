using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.RestraurantAdmin;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using NUnit.Framework;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "RestaurantAdminDomain integration tests")]
    public class RestaurantAdminDomainTest : DomainTest<RestaurantAdminDomain>
    {
        [Test(Description = "Get cuisines")]
        public void GetCuisines()
        {
            var cuisines = Domain.GetCuisines();

            Assert.IsNotNull(cuisines);
            Assert.IsTrue(cuisines.Any());
        }

        [Test(Description = "Get restaurant cuisines")]
        public void GetRestaurantCuisines()
        {
            Guid restaurantId = Guid.Parse("575B7C69-2FE2-4A1F-BE58-23714B09A0FA");
            var cuisines = Domain.GetRestaurantCuisines(restaurantId);

            Assert.IsNotNull(cuisines);
            Assert.IsTrue(cuisines.Any());
        }

        [TestCase(1, false, Description = "Add already existing cuisine to restaurant")]
        [TestCase(4, true, Description = "Add new cuisine to restaurant")]
        public void AddRestaurantCuisine(int cuisineId, bool shouldSucceed)
        {
            Guid restaurantId = Guid.Parse("575B7C69-2FE2-4A1F-BE58-23714B09A0FA");
            var result = Domain.AddRestaurantCuisine(restaurantId, cuisineId);

            using (var rep = RepositoryOf<RestaurantCuisine>())
            {
                var cuisines = rep.GetAll().Where(x => x.RestaurantId == restaurantId).List();
                Assert.IsTrue(cuisines.Any(x => x.CuisineId == cuisineId));
            }

            Assert.AreEqual(shouldSucceed, result != null);
        }


        [TestCase(6, Result = false, Description = "Remove not existing cuisine from restaurant")]
        [TestCase(4, Result = true, Description = "Remove existing cuisine from restaurant")]
        public bool RemoveRestaurantCuisine(int cuisineId)
        {
            Guid restaurantId = Guid.Parse("575B7C69-2FE2-4A1F-BE58-23714B09A0FA");
            bool result = Domain.RemoveRestaurantCuisine(restaurantId, cuisineId);

            using (var rep = RepositoryOf<RestaurantCuisine>())
            {
                var cuisines = rep.GetAll().Where(x => x.RestaurantId == restaurantId).List();
                Assert.IsFalse(cuisines.Any(x => x.CuisineId == cuisineId));
            }

            return result;
        }
    }
}
