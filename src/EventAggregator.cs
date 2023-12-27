using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using static System.Windows.Forms.DataFormats;
using System.Xml;

namespace HideSloth
{
    public interface IEventAggregator
    {
        void Subscribe<TEvent>(Action<TEvent> action);
        void Subscribe<TEvent>(EventHandler<TEvent> eventHandler) where TEvent : EventArgs;
        void Publish<TEvent>(TEvent eventToPublish);
    }
    public class SimpleEventAggregator : IEventAggregator
    {
        private static readonly SimpleEventAggregator _instance = new SimpleEventAggregator();
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        private SimpleEventAggregator() { }

        public static SimpleEventAggregator Instance => _instance;

        public void Subscribe<TEvent>(Action<TEvent> action)
        {
            SubscribeInternal(action);
        }

        public void Subscribe<TEvent>(EventHandler<TEvent> eventHandler) where TEvent : EventArgs
        {
            SubscribeInternal(eventHandler);
        }

        private void SubscribeInternal(Delegate del)
        {
            Type eventType = del.GetType().GetGenericArguments()[0];
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<Delegate>();
            }
            _subscribers[eventType].Add(del);
        }
        public void Publish<TEvent>(TEvent eventToPublish)
        {
            if (_subscribers.ContainsKey(typeof(TEvent)))
            {
                foreach (var subscriber in _subscribers[typeof(TEvent)])
                {
                    if (subscriber is Action<TEvent> action)
                    {
                        action(eventToPublish);
                    }
                    else if (subscriber is EventHandler<TEvent> eventHandler)
                    {
                        eventHandler(this, eventToPublish);
                    }
                }
            }
        }
    }


}
