using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Service.Client
{
	public class FoodSearchServiceClient : IFoodSearchServiceClient
	{
        public event EventHandler NoNetwork;

        public event EventHandler Unauthorized;

        public void OnNoNetwork()
        {
            var handler = NoNetwork;
            if (handler != null)
                NoNetwork(this, EventArgs.Empty);
        }

        public void OnUnauthorized()
        {
            var handler = Unauthorized;
            if (handler != null)
                Unauthorized(this, EventArgs.Empty);
        }

		private IFoodSearchCoreServiceClient _core;
        private IFoodSearchUserServiceClient _user;
        private IFoodSearchOrderServiceClient _order;

        public IFoodSearchCoreServiceClient Core { get { return _core ?? (_core = new FoodSearchCoreServiceClient ()); } }
        public IFoodSearchUserServiceClient User { get { return _user ?? (_user = new FoodSearchUserServiceClient ()); } }
        public IFoodSearchOrderServiceClient Order { get { return _order ?? (_order = new FoodSearchOrderServiceClient ()); } }
	}
}

