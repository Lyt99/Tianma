using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tianma.Exceptions
{
    class PluginLoadException : TianmaException
    {
        public PluginLoadException(string message) : base(message) { }
    }
}
