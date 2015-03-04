using FoodSearch.BusinessLogic.Domain.RestraurantAdmin;
using FoodSearch.Data.Mapping.Entities;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "RestaurantAdminDomain integration tests")]
    public class RestaurantAdminDomainTest : DomainTest<RestaurantAdminDomain>
    {
        [TestCase(TestName = "Get cuisines")]
        public void GetCuisines()
        {
            var cuisines = Domain.GetCuisines();

            Assert.IsNotNull(cuisines);
            Assert.IsTrue(cuisines.Any());
        }

        [TestCase(TestName = "Get restaurant cuisines")]
        public void GetRestaurantCuisines()
        {
            Guid restaurantId = Guid.Parse("575B7C69-2FE2-4A1F-BE58-23714B09A0FA");
            var cuisines = Domain.GetRestaurantCuisines(restaurantId);

            Assert.IsNotNull(cuisines);
            Assert.IsTrue(cuisines.Any());
        }

        [TestCase(1, false, TestName = "Add already existing cuisine to restaurant")]
        [TestCase(4, true, TestName = "Add new cuisine to restaurant")]
        public void AddRestaurantCuisine(int cuisineId, bool shouldSucceed)
        {
            Guid restaurantId = Guid.Parse("575B7C69-2FE2-4A1F-BE58-23714B09A0FA");
            var result = Domain.AddRestaurantCuisine(restaurantId, cuisineId);

            Assert.AreEqual(shouldSucceed, result != null);

            if (shouldSucceed)
            {
                using (var rep = RepositoryOf<RestaurantCuisine>())
                {
                    var restCuisine = rep.GetAll()
                        .Where(x => x.RestaurantId == restaurantId && x.CuisineId == cuisineId)
                        .SingleOrDefault();
                    Assert.IsNotNull(restCuisine);
                }
            }

        }


        [TestCase(6, Result = false, TestName = "Remove not existing cuisine from restaurant")]
        [TestCase(4, Result = true, TestName = "Remove existing cuisine from restaurant")]
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

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", true, TestName = "Get existing dish groups")]
        [TestCase("03C936DF-45C2-45FC-93CD-E55A8469964B", false, TestName = "Get dish groups from restaurant without them")]
        public void GetDishGroups(string restaurantIdString, bool expectedAny)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            var result = Domain.GetDishGroups(restaurantId);

            Assert.AreEqual(expectedAny, result.Any());
        }

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", "grupaTestowa1", false, TestName = "Try to create duplicated dish group")]
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", "grupaTestowa3", true, TestName = "Create new dish group")]
        public void CreateDishGroup(string restaurantIdString, string groupName, bool shouldSucceed)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            var result = Domain.CreateDishGroup(restaurantId, groupName);

            Assert.AreEqual(shouldSucceed, result != null);

            if (shouldSucceed)
            {
                using (var rep = RepositoryOf<DishGroup>())
                {
                    var dishGroup = rep.Get(result.Id);
                    Assert.IsNotNull(dishGroup);
                    Assert.AreEqual(groupName, dishGroup.Name);
                }
            }
        }

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 10, "wontWork", Result = false, TestName = "Edit non existing dish group")]
        [TestCase("03C936DF-45C2-45FC-93CD-E55A8469964B", 4, "wontWork", Result = false, TestName = "Edit existring dish group from another restaurant")]
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 4, "nowaNazwa", Result = true, TestName = "Edit existring dish group from right restaurant")]
        public bool EditDishGroup(string restaurantIdString, int dishGroupId, string dishGroupName)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            
            return Domain.EditDishGroup(restaurantId, dishGroupId, dishGroupName);
        }

        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 10, false, TestName = "Delete non existing dish group")]
        [TestCase("03C936DF-45C2-45FC-93CD-E55A8469964B", 4, false, TestName = "Delete existring dish group from another restaurant")]
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", 5, true, TestName = "Delete existring dish group from right restaurant")]
        public void DeleteDishGroup(string restaurantIdString, int dishGroupId, bool shouldSucceed)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            bool result = Domain.DeleteDishGroup(restaurantId, dishGroupId);

            Assert.AreEqual(shouldSucceed, result);

            if (shouldSucceed)
            {
                using (var rep = RepositoryOf<DishGroup>())
                {
                    DishGroup dg;
                    Assert.IsFalse(rep.TryGet(dishGroupId, out dg));
                }
            }
        }

        [TestCase("D3FE2D94-6ADE-4AB2-ADC6-06BEEB6D0AB2", false, TestName = "Get dishes from restaurant without them")]
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", true, TestName = "Get dishes list")]
        public void GetDishes(string restaurantIdString, bool shouldSucceed)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            var dishes = Domain.GetDishes(restaurantId);

            Assert.AreEqual(shouldSucceed, dishes.Any());
        }
        
        [TestCase("575B7C69-2FE2-4A1F-BE58-23714B09A0FA", "noweDanie", 4, Result = true, TestName = "Create new dish")]
        public bool CreateDish(string restaurantIdString, string dishName, int dishGroupId)
        {
            Guid restaurantId = Guid.Parse(restaurantIdString);
            var newDish = Domain.CreateDish(restaurantId, dishName, dishGroupId, 10m, 1);
            bool result = newDish != null;

            if (result)
            {
                using (var rep = RepositoryOf<Dish>())
                {
                    var dish = rep.Get(newDish.Id);
                    
                    Assert.IsNotNull(dish);
                    Assert.AreEqual(restaurantId, dish.RestaurantId);
                    Assert.AreEqual(dishName, dish.DishName);
                    Assert.AreEqual(dishGroupId, dish.DishGroupId);
                }
            }
            return result;
        }

        [TestCase(TestName = "Get restaurant opening hours")]
        public void GetOpeningHours()
        {
            Guid restaurantId = Guid.Parse("575B7C69-2FE2-4A1F-BE58-23714B09A0FA");
            var openingHours = Domain.GetOpeningHours(restaurantId);

            Assert.IsTrue(openingHours.Any());
        }

        [TestCase(5, "20:00", "21:30", Result = true, TestName = "Create valid opening hour")]
        [TestCase(5, "22:00", "23:00", Result = true, TestName = "Create valid opening hour 2")]
        [TestCase(1, "06:30", "10:00", Result = false, TestName = "Try creating overlapping opening hour 1")]
        [TestCase(1, "18:00", "21:00", Result = false, TestName = "Try creating overlapping opening hour 2")]
        [TestCase(1, "08:30", "10:00", Result = false, TestName = "Try creating overlapping opening hour 3")]
        [TestCase(1, "07:00", "07:00", Result = false, TestName = "Try creating opening hour with same hours 1")]
        [TestCase(1, "10:00", "10:00", Result = false, TestName = "Try creating opening hour with same hours 2")]
        [TestCase(1, "20:00", "20:00", Result = false, TestName = "Try creating opening hour with same hours 3")]
        [TestCase(1, "21:00", "21:00", Result = false, TestName = "Try creating opening hour with same hours 4")]
        [TestCase(1, "13:00", "11:00", Result = false, TestName = "Try creating opening hour with timeTo < timeFrom")]
        public bool CreateOpeningHour(int day, string timeFromString, string timeToString)
        {
            Guid restaurantId = Guid.Parse("575B7C69-2FE2-4A1F-BE58-23714B09A0FA");
            TimeSpan timeFrom = TimeSpan.ParseExact(timeFromString, "g", CultureInfo.InvariantCulture);
            TimeSpan timeTo = TimeSpan.ParseExact(timeToString, "g", CultureInfo.InvariantCulture);

            var openingHour = Domain.CreateOpeningHour(restaurantId, day, timeFrom, timeTo);
            return openingHour != null;
        }

        [TestCase(3, true, TestName = "Delete existing opening hour")]
        [TestCase(1000, false, TestName = "Delete non existing opening hour")]
        public void DeleteOpeningHour(int openingId, bool shouldSucceed)
        {
            bool result = Domain.DeleteOpeningHour(openingId);
            Assert.AreEqual(shouldSucceed, result);

            using (var rep = RepositoryOf<OpeningHour>())
            {
                OpeningHour oh;
                Assert.IsFalse(rep.TryGet(openingId, out oh));
            }
        }
    }
}
