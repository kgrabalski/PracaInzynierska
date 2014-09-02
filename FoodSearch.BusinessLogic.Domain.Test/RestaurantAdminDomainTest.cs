using FoodSearch.BusinessLogic.Domain.RestraurantAdmin;
using FoodSearch.Data.Mapping.Entities;
using NUnit.Framework;
using System;
using System.Linq;

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

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", true, Description = "Get existing dish groups")]
        [TestCase("03C936DF-45C2-45FC-93CD-E55A8469964B", false, Description = "Get dish groups from restaurant without them")]
        public void GetDishGroups(string restaurantIdString, bool expectedAny)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            var result = Domain.GetDishGroups(restaurantId);

            Assert.AreEqual(expectedAny, result.Any());
        }

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", "grupaTestowa1", false, Description = "Try to create duplicated dish group")]
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", "grupaTestowa3", true, Description = "Create new dish group")]
        public void CreateDishGroup(string restaurantIdString, string groupName, bool shouldSucceed)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            var result = Domain.CreateDishGroup(restaurantId, groupName);

            Assert.AreEqual(shouldSucceed, result != null);
        }

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 10, "wontWork", Result = false, Description = "Edit non existing dish group")]
        [TestCase("03C936DF-45C2-45FC-93CD-E55A8469964B", 4, "wontWork", Result = false, Description = "Edit existring dish group from another restaurant")]
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 4, "nowaNazwa", Result = true, Description = "Edit existring dish group from right restaurant")]
        public bool EditDishGroup(string restaurantIdString, int dishGroupId, string dishGroupName)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            
            return Domain.EditDishGroup(restaurantId, dishGroupId, dishGroupName);
        }

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 10, false, Description = "Delete non existing dish group")]
        [TestCase("03C936DF-45C2-45FC-93CD-E55A8469964B", 4, false, Description = "Delete existring dish group from another restaurant")]
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 5, true, Description = "Delete existring dish group from right restaurant")]
        public void DeleteDishGroup(string restaurantIdString, int dishGroupId, bool shouldSucceed)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            bool result = Domain.DeleteDishGroup(restaurantId, dishGroupId);

            Assert.AreEqual(shouldSucceed, result);
        }
    }
}
