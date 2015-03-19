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
            using (var repOd = RepositoryOf<FoodSearch.Data.Mapping.Entities.OrderDish>())
            using (var repD = RepositoryOf<FoodSearch.Data.Mapping.Entities.Dish>())
            {
                var orderQuery = repO.Get(order.OrderId);
                Assert.IsNotNull(orderQuery);

                var itemsQuery = repOd.GetAll()
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

        [TestCase(TestName = "Get payment types")]
        public void GetPaymentTypes()
        {
            var types = Domain.GetPaymentTypes();

            Assert.AreEqual(2, types.Count());
        }

        [TestCase(TestName = "Get delivery types")]
        public void GetDeliveryTypes()
        {
            var types = Domain.GetDeliveryTypes();

            Assert.AreEqual(2, types.Count());
        }

        [TestCase(10, 3, TestName = "Get delivery price for order below free delivery amount")]
        [TestCase(50, 0, TestName = "Get delivery price for order above free delivery amount")]
        public void GetDeliveryPrice(decimal orderAmount, decimal expectedDeliveryPrice)
        {
            Guid restaurantId = Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686");
            decimal deliveryPrice = Domain.GetDeliveryPrice(restaurantId, orderAmount);

            Assert.AreEqual(expectedDeliveryPrice, deliveryPrice);
        }

        [TestCase(TestName = "Update payment")]
        public void UpdatePayment()
        {
            Guid paymentId = Guid.Parse("698524C1-391B-474D-948F-74D74BAE7D59");
            bool result = Domain.UpdatePayment(paymentId, PaymentStates.Completed);

            Assert.IsTrue(result);

            using (var rep = RepositoryOf<Payment>())
            using (var repH = RepositoryOf<PaymentHistory>())
            {
                var payment = rep.Get(paymentId);

                Assert.IsNotNull(payment);
                Assert.AreEqual((int)PaymentStates.Completed, payment.PaymentStateId);

                var history = repH.GetAll()
                    .Where(x => x.PaymentId == paymentId)
                    .OrderBy(x => x.ModificationDate).Desc
                    .Take(1)
                    .List()
                    .FirstOrDefault();

                Assert.IsNotNull(history);
                Assert.AreEqual((int) PaymentStates.Completed, history.PaymentStateId);
            }
        }

        [TestCase("698524C1-391B-474D-948F-74D74BAE7D59", "96B00CCC-4350-4D8A-AB06-843809AE4F72", TestName = "Get order for payment 1")]
        [TestCase("6576E6B8-5577-4068-8856-B16D77E00276", "59853C05-1B3E-45C5-A562-07F1D3834249", TestName = "Get order for payment 2")]
        public void GetOrderForPayment(string paymentIdString, string expectedOrderIdString)
        {
            Guid restaurantId = Guid.Parse("B219B818-2B58-4295-8871-FBBA6D4EE686");
            Guid paymentId = Guid.Parse(paymentIdString);
            Guid expectedOrderId = Guid.Parse(expectedOrderIdString);

            var order = Domain.GetOrderForPayment(paymentId);

            Assert.AreEqual(restaurantId, order.RestaurantId);
            Assert.AreEqual(expectedOrderId, order.OrderId);
        }

        [TestCase(TestName = "Change order status")]
        public void ChangeOrderState()
        {
            Guid orderId = Guid.Parse("96B00CCC-4350-4D8A-AB06-843809AE4F72");

            bool result = Domain.ChangeOrderState(orderId, OrderStates.Paid);

            Assert.IsTrue(result);

            using (var rep = RepositoryOf<Data.Mapping.Entities.Order>())
            {
                var order = rep.Get(orderId);

                Assert.IsNotNull(order);
                Assert.AreEqual((int) OrderStates.Paid, order.OrderStateId);
            }
        }

        [TestCase(TestName = "Get order delivery status")]
        public void GetDeliveryStatus()
        {
            Guid orderId = Guid.Parse("96B00CCC-4350-4D8A-AB06-843809AE4F72");
            var status = Domain.GetDeliveryStatus(orderId);

            Assert.IsNotNull(status);
            Assert.AreEqual(ConfirmationStatus.Completed, status.ConfirmationStatus);
        }
    }
}
