using System;

namespace FoodSearch.Service.Client
{
    public class FoodSearchOrderServiceClient : ServiceClientBase, IFoodSearchOrderServiceClient
    {
        protected override string ServiceAddress
        {
            get { return _baseAddress + "Order/"; }
        }
    }
}

