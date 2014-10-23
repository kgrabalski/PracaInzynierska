using System;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IServiceClient
    {
        event EventHandler NoNetwork;
        event EventHandler Unauthorized;
    }
}

