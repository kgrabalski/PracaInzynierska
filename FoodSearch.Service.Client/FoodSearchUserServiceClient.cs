using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Service.Client
{
    public class FoodSearchUserServiceClient : ServiceClientBase, IFoodSearchUserServiceClient
    {
        protected override string ServiceAddress
        {
            get { return _baseAddress + "User/"; }
        }
    }
}
