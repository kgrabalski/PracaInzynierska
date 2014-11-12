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

        public IFoodSearchCoreServiceClient Core { get { return _core ?? (_core = new FoodSearchCoreServiceClient (_cookieContainer) {ClientHandler = this}); } }
        public IFoodSearchUserServiceClient User { get { return _user ?? (_user = new FoodSearchUserServiceClient (_cookieContainer) {ClientHandler = this}); } }
        public IFoodSearchOrderServiceClient Order { get { return _order ?? (_order = new FoodSearchOrderServiceClient(_cookieContainer) {ClientHandler = this}); } }

        public event EventHandler UnauthorizedAccess;
        public CookieContainer Cookies { get { return _cookieContainer; } }

        public void OnUnauthorizedAccess()
        {
            var handler = UnauthorizedAccess;
            if (handler != null) UnauthorizedAccess(this, EventArgs.Empty);
        }
	}
}

