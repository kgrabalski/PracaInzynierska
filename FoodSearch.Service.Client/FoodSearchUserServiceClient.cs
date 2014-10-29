using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Service.Client
{
    public class FoodSearchUserServiceClient : ServiceClientBase, IFoodSearchUserServiceClient
    {
        public FoodSearchUserServiceClient(CookieContainer cookieContainer) : base(cookieContainer)
        {
        }

        protected override string ServiceAddress
        {
            get { return _baseAddress + "User/"; }
        }
    }
}
