using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

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