
using FoodSearch.Data.Mapping.Entities;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace FoodSearch.Data.Mapping.Test
{
    [TestFixture(Category = "DataMapping Test")]
    public class EntitiesTest : DbTest
    {
        private readonly MethodInfo _repositoryGenericMethod;

        public EntitiesTest()
        {
            _repositoryGenericMethod = GetType().GetMethods().First(x => x.IsGenericMethod);
        }

        [TestCase(typeof(Address), TestName = "Address")]
        [TestCase(typeof(City), TestName = "City")]
        [TestCase(typeof(Cuisine), TestName = "Cuisine")]
        [TestCase(typeof(DeliveryType), TestName = "DeliveryType")]
        [TestCase(typeof(Dish), TestName = "Dish")]
        [TestCase(typeof(DishGroup), TestName = "DishGroup")]
        [TestCase(typeof(District), TestName = "District")]
        [TestCase(typeof(Image), TestName = "Image")]
        [TestCase(typeof(OpeningHour), TestName = "OpeningHour")]
        [TestCase(typeof(Opinion), TestName = "Opinion")]
        [TestCase(typeof(Order), TestName = "Order")]
        [TestCase(typeof(OrderDish), TestName = "OrderDish")]
        [TestCase(typeof(Payment), TestName = "Payment")]
        [TestCase(typeof(PaymentHistory), TestName = "PaymentHistory")]
        [TestCase(typeof(PaymentState), TestName = "PaymentState")]
        [TestCase(typeof(PaymentType), TestName = "PaymentType")]
        [TestCase(typeof(RegistrationConfirm), TestName = "RegistrationConfirm")]
        [TestCase(typeof(Restaurant), TestName = "Restaurant")]
        [TestCase(typeof(RestaurantCuisine), TestName = "RestaurantCuisine")]
        [TestCase(typeof(RestaurantUser), TestName = "RestaurantUser")]
        [TestCase(typeof(Street), TestName = "Street")]
        [TestCase(typeof(User), TestName = "User")]
        [TestCase(typeof(UserState), TestName = "UserState")]
        [TestCase(typeof(UserType), TestName = "UserType")]
        public void TableDataMapping(Type table)
        {
            using (dynamic rep = _repositoryGenericMethod.MakeGenericMethod(table).Invoke(this, null))
            {
                var query = rep.GetAll();
                Type qt = query.GetType();
                var list = qt.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .First(x => x.Name == "NHibernate.IQueryOver<TRoot>.List" && !x.IsGenericMethod);
                dynamic tableData = list.Invoke(query, null);

            }
        }
    }
}
