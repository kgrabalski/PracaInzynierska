using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public static class MessageService
    {
        private static readonly Dictionary<Type, object> _actions = new Dictionary<Type, object>();

        public static void Register<T>(Action<T> receiveAction)
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

        public static void Send<T>(T message)
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
