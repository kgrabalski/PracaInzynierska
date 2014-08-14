using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.User;
using FoodSearch.BusinessLogic.Domain.User.Interface;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using NUnit.Framework;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "UserDomain integration tests")]
    public class UserDomainTest : DomainTest<UserDomain>
    {

    }
}
