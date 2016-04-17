using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eeNet
{
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException() : base() { }
        public DoesNotExistException(string message) : base(message) { }
        public DoesNotExistException(string message, System.Exception inner) : base(message, inner) { }
    }
}
