using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tianma.Exceptions
{
    public class PluginLoadException : TianmaException
    {
        public PluginLoadException(string message) : base(message) { }
    }
}
