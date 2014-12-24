using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FoodSearch.Presentation.Web.Site.WebSocket
{
    [HubName("FoodSearchMobile")]
    public class FoodSearchMobileHub : Hub
    {
        public void Register(Guid paymentId)
        {
            Groups.Add(Context.ConnectionId, paymentId.ToString());
        }

        public void Unregister(Guid paymentId)
        {
            Groups.Remove(Context.ConnectionId, paymentId.ToString());
        }
    }
}