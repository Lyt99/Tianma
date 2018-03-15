using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Tianma.Models
{
    class EventInvoker
    {
        private MethodBase mb = null;
        private object instance = null;

        public delegate void InvokerDelegate(object[] pars);

        public static MethodInfo getMethodInfo()
        {
            return Harmony.AccessTools.Method(typeof(EventInvoker), "Invoke");
        }

        public EventInvoker(MethodBase mb, object instance = null)
        {
            this.mb = mb;
            this.instance = instance;
        }

        public void Invoke(object[] pars)
        {
            mb.Invoke(instance, pars);
        }

    }
}
