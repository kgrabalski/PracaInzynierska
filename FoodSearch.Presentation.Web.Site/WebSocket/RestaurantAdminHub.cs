﻿using FoodSearch.Presentation.Web.Site.Helpers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace FoodSearch.Presentation.Web.Site.WebSocket
{
    [HubName("FoodSearchRestaurantAdmin")]
    [AreaAuthorize(Roles = "RestaurantEmployee, RestaurantAdmin")]
    public class RestaurantAdminHub : Hub
    {
        public void RegisterRestaurant(Guid restaurantId)
        {
            Groups.Add(Context.ConnectionId, restaurantId.ToString());
        }

        public void UnregisterRestaurant(Guid restaurantId)
        {
            Groups.Remove(Context.ConnectionId, restaurantId.ToString());
        }
    }
}