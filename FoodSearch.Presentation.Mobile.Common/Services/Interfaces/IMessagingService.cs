using System;

namespace FoodSearch.Presentation.Mobile.Common.Services.Interfaces
{
    public interface IMessagingService
    {
        void Register<T>(Action<T> receiveAction);
        void Send<T>(T message);
    }
}

