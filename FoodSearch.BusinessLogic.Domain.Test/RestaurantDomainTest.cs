using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Restaurant;
using FoodSearch.Data.Mapping.Test;

using NHibernate.Exceptions;

using NUnit.Framework;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "RestaurantDomain intergrations tests")]
    public class RestaurantDomainTest : DomainTest<RestaurantDomain>
    {
        [TestCase(TestName = "Get restaurants for Lewartowskiego 17")]
        public void GetRestaurants()
        {
            var restaurants = Domain.GetRestaurants(1315, DateTime.Now);

            Assert.IsTrue(restaurants.Any());
        }

        [TestCase("B219B818-2B58-4295-8871-FBBA6D4EE686", "Restauracja 223", TestName = "Get restaurant name for Restauracja 223")]
        [TestCase("A6F011F6-A7EF-47B6-AD3A-DD1057CE8BEE", "Restauracja 250", TestName = "Get restaurant name for Restauracja 250")]
        [TestCase("00000000-0000-0000-0000-000000000000", null, TestName = "Get restaurant name for not existing id")]
        public void GetRestaurantName(string idString, string expectedName)
        {
            Guid restaurantId = Guid.Parse(idString);
            string name = Domain.GetRestaurantName(restaurantId);

            Assert.AreEqual(expectedName, name);
        }

        [TestCase(TestName = "Get restaurant dishes")]
        public void GetDishes()
        {
            var dishes = Domain.GetDishes(Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686"));

            Assert.IsTrue(dishes.Any());
        }

        [TestCase(TestName = "Get dish")]
        public void GetDish()
        {
            var dish = Domain.GetDish(2103);
            
            Assert.IsNotNull(dish);
            Assert.AreEqual("Pizza Pepperoni", dish.Name);
        }

        [TestCase(TestName = "Get partner restaurants")]
        public void GetPartnersRestaurants()
        {
            var restaurants = Domain.GetPartnerRestaurants();

            Assert.IsTrue(restaurants.Any());
        }

        [TestCase(TestName = "Get restaurant opinions")]
        public void GetRestaurantOpinions()
        {
            var opinions = Domain.GetOpinions(Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686"));

            Assert.IsTrue(opinions.Any());
        }

        [TestCase("E808EC47-6FD4-42E8-BFFA-113AE459387B", 1, false, TestName = "Add opinion from user who has already added opinion")]
        [TestCase("00000000-0000-0000-0000-000000000000", 1, false, TestName = "Add opinion from non existing user", ExpectedException = typeof(GenericADOException))]
        [TestCase("A08A5489-36B5-48CB-9F6A-91886578E688", 5, true, TestName = "Add opinion from user who has not already added opinion")]
        [TestCase("B60C6C65-AD42-475C-A0EB-9E8CE72D9A45", 50, false, TestName = "Add opinion with non valid rating")]
        public void AddRestaurantOpinion(string userIdString, int rating, bool shouldSucceedd)
        {
            Guid restaurantId = Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686");
            bool result = Domain.AddOpinion(restaurantId, Guid.Parse(userIdString), rating, "test");

            Assert.AreEqual(shouldSucceedd, result);
        }

        [TestCase(TestName = "Get restaurant details")]
        public void GetRestaurantDetails()
        {
            var details = Domain.GetRestaurantDetails(Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686"));

            Assert.AreEqual("Restauracja 223", details.RestaurantName);
        }

        [TestCase("7E7BB9B0-C344-48B0-8529-510EC9460300", true, TestName = "Check if user has commented restaurant")]
        [TestCase("426692DB-72D6-42B8-8796-27D5F30BD415", false, TestName = "Check if user has commented restaurant")]
        public void CheckUserCommentExists(string userIdString, bool shouldExist)
        {
            Guid restaurantId = Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686");
            Guid userId = Guid.Parse(userIdString);
            bool result = Domain.CheckUserCommentExists(userId, restaurantId);

            Assert.AreEqual(shouldExist, result);
        }
    }
}
