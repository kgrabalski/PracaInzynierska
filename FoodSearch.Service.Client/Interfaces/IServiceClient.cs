using System;

namespace FoodSearch.Service.Client
{
    public interface IServiceClient
    {
        event EventHandler NoNetwork;
        event EventHandler Unauthorized;
    }
}

