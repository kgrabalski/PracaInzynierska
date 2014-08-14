using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.RestraurantAdmin;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface;
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
            Assert.IsTrue(cuisines.Any());
        }
    }
}
