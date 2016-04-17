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

        private Dictionary<string, List<Action<object>>> _events;

        /// <summary>
        /// The EventEmitter object to subscribe to events with
        /// </summary>
        public EventEmitter()
        {
            this._events = new Dictionary<string, List<Action<object>>>();
        }

        /// <summary>
        /// Whenever eventName is emitted, the functions attached to this event will be called
        /// </summary>
        /// <param name="eventName">Event name to subscribe to</param>
        /// <param name="func">Function to add to the event</param>
        public void On(string eventName, Action<object> func)
        {
            List<Action<object>> subscribedFuncs;
            if (this._events.TryGetValue(eventName, out subscribedFuncs))
            {
                subscribedFuncs.Add(func);
            }
            else
            {
                this._events.Add(eventName, new List<Action<object>> { func });
            }
        }

        /// <summary>
        /// Emits the event and associated data
        /// </summary>
        /// <param name="eventName">Event name to be emitted</param>
        /// <param name="data">Data to call the attached functions with</param>
        public void Emit(string eventName, object data)
        {
            List<Action<object>> subscribedFuncs;
            if (!this._events.TryGetValue(eventName, out subscribedFuncs))
            {
                throw new DoesNotExistException(string.Format("Event [{0}] does not exist in the emitter. Consider calling EventEmitter.On", eventName));
            }
            else
            {
                foreach (var f in subscribedFuncs)
                {
                    f(data);
                }
            }
        }

        /// <summary>
        /// Removes the function [func] from the event
        /// </summary>
        /// <param name="eventName">Event name to remove function from</param>
        /// <param name="func">Function to remove from the event name</param>
        public void RemoveListener(string eventName, Action<object> func)
        {
            List<Action<object>> subscribedFuncs;
            if (!this._events.TryGetValue(eventName, out subscribedFuncs))
            {
                throw new DoesNotExistException(string.Format("Event [{0}] does not exist to have listeners removed.", eventName));
            }
            else
            {
                var _event = subscribedFuncs.Exists(e => e == func);
                if (_event == false)
                {
                    throw new DoesNotExistException(string.Format("Func [{0}] does not exist to be removed.", func.Method));
                }
                else
                {
                    subscribedFuncs.Remove(func);
                }                
            }
        }

        /// <summary>
        /// Removes all functions from the event [eventName]
        /// </summary>
        /// <param name="eventName">Event name to remove functions from</param>
        public void RemoveAllListeners(string eventName)
        {
            List<Action<object>> subscribedFuncs;
            if (!this._events.TryGetValue(eventName, out subscribedFuncs))
            {
                throw new DoesNotExistException(string.Format("Event [{0}] does not exist to have listeners removed.", eventName));
            }
            else
            {
                subscribedFuncs.RemoveAll(x => x != null);
            }
        }
    }
}
