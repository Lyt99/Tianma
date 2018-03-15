using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tianma.Exceptions
{
    class TianmaException : Exception
    {
        public TianmaException(string message) : base(message) { }
    }
}
