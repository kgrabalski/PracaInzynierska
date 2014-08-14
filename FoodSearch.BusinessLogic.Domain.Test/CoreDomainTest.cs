using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Core;
using FoodSearch.BusinessLogic.Domain.Core.Interface;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using NUnit.Framework;

namespace FoodSearch.BusinessLogic.Domain.Test
{
    [TestFixture(Category = "CoreDomain intergrations tests")]
    public class CoreDomainTest : DomainTest<CoreDomain>
    {

    }
}
