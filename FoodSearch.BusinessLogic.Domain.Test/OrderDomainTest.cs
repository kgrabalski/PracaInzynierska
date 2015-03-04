using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Order;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;

using NUnit.Framework;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "Order integration tests")]
    public class OrderDomainTest : DomainTest<OrderDomain>
    {
        [TestCase(TestName = "Create order")]
        public void CreateOrder()
        {
            Guid userId = Guid.Parse("CA7D09BF-ED58-4083-B1F5-8F21EF735229");
            Guid restaurantId = Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686");
            var dishes = new []{2103, 2112}.Select(x => new OrderItem() {DishId = x, Quantity = 1}).ToList();
            int deliveryAddressId = 501;

            var order = Domain.CreateOrder(userId, restaurantId, dishes, DeliveryTypes.Shipping, PaymentTypes.Cash, deliveryAddressId);

            Assert.IsNotNull(order);
            Assert.AreNotEqual(Guid.Empty, order.OrderId);
            Assert.AreNotEqual(Guid.Empty, order.PaymentId);

            using (var repO = RepositoryOf<FoodSearch.Data.Mapping.Entities.Order>())
            using (var repP = RepositoryOf<FoodSearch.Data.Mapping.Entities.Payment>())
            using (var repOD = RepositoryOf<FoodSearch.Data.Mapping.Entities.OrderDish>())
            using (var repD = RepositoryOf<FoodSearch.Data.Mapping.Entities.Dish>())
            {
                var orderQuery = repO.Get(order.OrderId);
                Assert.IsNotNull(orderQuery);

                var itemsQuery = repOD.GetAll()
                    .Where(x => x.OrderId == order.OrderId)
                    .List()
                    .ToList();
                Assert.AreEqual(2, itemsQuery.Count);

                var dishQuery = repD.GetAll()
                    .WhereRestrictionOn(x => x.DishId)
                    .IsIn(dishes.Select(x => x.DishId).ToList())
                    .List()
                    .ToList();
                decimal amount = dishQuery.Sum(x => x.Price);

                var paymentQuery = repP.Get(order.PaymentId);
                Assert.IsNotNull(paymentQuery);
                Assert.AreEqual(amount, paymentQuery.Amount);
            }
        }

        [TestCase(TestName = "Try create empty order")]
        public void CreateEmptyOrder()
        {
            Guid userId = Guid.Parse("CA7D09BF-ED58-4083-B1F5-8F21EF735229");
            Guid restaurantId = Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686");
            int deliveryAddressId = 501;

            var order = Domain.CreateOrder(userId, restaurantId, new List<OrderItem>(), DeliveryTypes.Shipping, PaymentTypes.Cash, deliveryAddressId);
            Assert.IsNull(order);
        }
    }
}
