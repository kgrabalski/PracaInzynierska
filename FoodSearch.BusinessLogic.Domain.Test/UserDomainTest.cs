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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestClass]
    public class UserDomainTest
    {
        private ISessionSource sessionSource;
        private IRepositoryProvider provider;
        private IUserDomain domain;

        [TestInitialize]
        public void BusinessLogic_Domain_User_InitializeTest()
        {
            sessionSource = new SessionSource(Databases.FoodSearchTest);
            provider = new FoodSearchRepositoryProvider(sessionSource);
            domain = new UserDomain(provider);
        }

        [TestMethod]
        public void BusinessLogic_Domain_User_CreateUser()
        {
            SHA256 sha = new SHA256Cng();
            var passwordHash = sha.ComputeHash(Encoding.UTF8.GetBytes("admin"));
            var userId = domain.CreateUser("admin", "Krzysztof", "Grabalski", "kgrabalski@poczta.onet.pl", passwordHash);
        }
    }
}
