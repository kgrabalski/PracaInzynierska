using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly Dictionary<Type, object> _actions = new Dictionary<Type, object>();

        public void Register<T>(Action<T> receiveAction)
        {
            var messageType = typeof (T);
            if (!_actions.ContainsKey(messageType))
            {
                _actions.Add(messageType, receiveAction);
            }
            else
            {
                _actions[messageType] = receiveAction;
            }
        }

        public void Send<T>(T message)
        {
            var messageType = typeof (T);
            if (_actions.ContainsKey(messageType))
            {
                var action = _actions[messageType] as Action<T>;
                if (action != null) action(message);
            }
        }
    }
}
