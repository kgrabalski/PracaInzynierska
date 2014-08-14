using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.SiteAdmin;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using NUnit.Framework;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "SiteAdminDomain integration tests")]
    public class SiteAdminDomainTest : DomainTest<SiteAdminDomain>
    {

    }
}
