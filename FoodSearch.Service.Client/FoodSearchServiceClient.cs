using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Service.Client
{
	public class FoodSearchServiceClient : IFoodSearchServiceClient
	{
        private readonly CookieContainer _cookieContainer = new CookieContainer();

		private IFoodSearchCoreServiceClient _core;
        private IFoodSearchUserServiceClient _user;
        private IFoodSearchOrderServiceClient _order;

        public IFoodSearchCoreServiceClient Core { get { return _core ?? (_core = new FoodSearchCoreServiceClient (_cookieContainer)); } }
        public IFoodSearchUserServiceClient User { get { return _user ?? (_user = new FoodSearchUserServiceClient (_cookieContainer)); } }
        public IFoodSearchOrderServiceClient Order { get { return _order ?? (_order = new FoodSearchOrderServiceClient(_cookieContainer)); } }
	}
}

