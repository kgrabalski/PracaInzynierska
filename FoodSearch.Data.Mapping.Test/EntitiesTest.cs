
using System;
using System.Linq;
using System.Reflection;

using FoodSearch.Data.Mapping.Entities;

using NUnit.Framework;

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

        [TestCase(typeof(Address))]
        [TestCase(typeof(City))]
        [TestCase(typeof(Cuisine))]
        [TestCase(typeof(DeliveryType))]
        [TestCase(typeof(Dish))]
        [TestCase(typeof(DishGroup))]
        [TestCase(typeof(District))]
        [TestCase(typeof(Image))]
        [TestCase(typeof(OpeningHour))]
        [TestCase(typeof(Opinion))]
        [TestCase(typeof(Order))]
        [TestCase(typeof(OrderDish))]
        [TestCase(typeof(Payment))]
        [TestCase(typeof(PaymentHistory))]
        [TestCase(typeof(PaymentState))]
        [TestCase(typeof(PaymentType))]
        [TestCase(typeof(RegistrationConfirm))]
        [TestCase(typeof(Restaurant))]
        [TestCase(typeof(RestaurantCuisine))]
        [TestCase(typeof(RestaurantUser))]
        [TestCase(typeof(Street))]
        [TestCase(typeof(User))]
        [TestCase(typeof(UserState))]
        [TestCase(typeof(UserType))]
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
