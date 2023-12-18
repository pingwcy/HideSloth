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
        void Publish<TEvent>(TEvent eventToPublish);
    }
    public class SimpleEventAggregator : IEventAggregator
    {
        private static readonly SimpleEventAggregator _instance = new SimpleEventAggregator();
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        // 私有构造函数，确保外部无法实例化
        private SimpleEventAggregator() { }

        // 提供一个公共静态方法访问实例
        public static SimpleEventAggregator Instance => _instance;

        public void Subscribe<TEvent>(Action<TEvent> action)
        {
            if (!_subscribers.ContainsKey(typeof(TEvent)))
            {
                _subscribers[typeof(TEvent)] = new List<Delegate>();
            }
            _subscribers[typeof(TEvent)].Add(action);
        }

        public void Publish<TEvent>(TEvent eventToPublish)
        {
            if (_subscribers.ContainsKey(typeof(TEvent)))
            {
                foreach (var action in _subscribers[typeof(TEvent)])
                {
                    ((Action<TEvent>)action)(eventToPublish);
                }
            }
        }
    }


}
