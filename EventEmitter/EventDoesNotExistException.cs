using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eeNet
{
    public class EventDoesNotExistException : Exception
    {
        public EventDoesNotExistException() : base() { }
        public EventDoesNotExistException(string message) : base(message) { }
        public EventDoesNotExistException(string message, System.Exception inner) : base(message, inner) { }
    }
}
