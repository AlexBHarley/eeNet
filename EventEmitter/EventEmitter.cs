using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eeNet
{ 
    public class EventEmitter
    {
        /*
        {
            KEY: "subscribe_event",
            VALUE: [
                HandleSubscribe<List<object>>,
                DoDbWork<List<object>>,
                SendInfo<List<object>>
            ],
            KEY: "listen_event",
            VALUE: [
                HandleListen<List<object>>
            ]
        }
        */
        private Dictionary<string, List<Action<List<object>>>> _events;

        public EventEmitter()
        {
            this._events = new Dictionary<string, List<Action<List<object>>>>();
        }

        internal void On(string eventName, Action<List<object>> func)
        {
            List<Action<List<object>>> subscribedFuncs;
            if (this._events.TryGetValue(eventName, out subscribedFuncs))
            {
                subscribedFuncs.Add(func);
            }
            else
            {
                this._events.Add(eventName, new List<Action<List<object>>> { func });
            }
        }

        internal void Emit(string eventName, List<object> data)
        {
            List<Action<List<object>>> subscribedFuncs;
            if (!this._events.TryGetValue(eventName, out subscribedFuncs))
            {
                throw new EventDoesNotExistException(string.Format("Event [{0}] does not exist. Consider calling EventEmitter.On"));
            }
            else
            {
                foreach (var f in subscribedFuncs)
                {
                    f(data);
                }
            }
            
        }
    }
}
