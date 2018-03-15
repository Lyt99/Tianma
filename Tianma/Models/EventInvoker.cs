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
            UnityEngine.Debug.Log(String.Format("Invoking {0} (par[0]: {1})", this.mb, pars[0]));
            mb.Invoke(instance, pars);
        }

    }
}
