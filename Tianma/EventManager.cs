using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Tianma.Models;
using Tianma.API;

namespace Tianma
{
    /// <summary>
    /// 注册、分发、管理事件
    /// </summary>
    class EventManager
    {
        //但是这样就只能用MethodBase注册事件了，尬的要死

        public static readonly EventManager INSTANCE = new EventManager();

        private Dictionary<EventType, Delegate> events = new Dictionary<EventType, Delegate>();


        /// <summary>
        /// 为某个实例根据Attribute注册所有事件
        /// </summary>
        /// <param name="typeOfInstance"></param>
        public void RegisterAll(object instance)
        {
            Type type = instance.GetType();
            var ptr = type.GetMethods().Where((p) => p.GetCustomAttributes(typeof(API.Attributes.RegisterEventAttribute), true).Any());

            foreach(MethodInfo i in ptr)
            {
                API.Attributes.RegisterEventAttribute attr = i.GetCustomAttributes(typeof(API.Attributes.RegisterEventAttribute), true).First() as API.Attributes.RegisterEventAttribute;
                if (attr.Check(i))
                {
                    this.RegisterEvent(attr.EventName, attr.EventTag, instance, i);
                }
                else
                {
                    throw new Exceptions.PluginLoadException(String.Format("注册事件 {0} 失败，方法检查未通过", attr.ToString()));
                }
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="method">方法</param>
        public bool RegisterEvent(string eventName, string eventTag, Action method)
        {
            return this.RegisterEvent(eventName, eventTag, (Delegate)method);
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="method">方法</param>
        /// <returns></returns>
        public bool RegisterEvent(string eventName, string eventTag, Delegate method)
        {
            try
            {
                var ptr = events.Where((p) => p.Key.EventName == eventName && p.Key.EventTag == p.Key.EventTag);
                if (ptr.Any())
                {
                    events[ptr.First().Key] = Delegate.Combine(events[ptr.First().Key], method);
                }
                else
                {
                    EventType et = new EventType() { EventName = eventName, EventTag = eventTag };
                    events.Add(et, method);
                }

                return true;
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="classInstance">类的实例</param>
        /// <param name="method">方法</param>
        /// <returns></returns>
        public bool RegisterEvent(string eventName, string eventTag, object classInstance, MethodInfo method)
        {
            try
            {
                EventInvoker ei = new EventInvoker(method, classInstance);
                Delegate @delegate = Delegate.CreateDelegate(typeof(EventInvoker.InvokerDelegate), ei, EventInvoker.getMethodInfo());
                return this.RegisterEvent(eventName, eventTag, @delegate);
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }

        /// <summary>
        /// 推送事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="args">参数</param>
        public void InvokeEvent(string eventName, string eventTag, params object[] args)
        {
            //如果存在再推送
            var ptr = events.Where((p) => p.Key.EventName == eventName && (p.Key.EventTag == p.Key.EventTag || p.Key.EventTag == null));//如果EventTag是null的，默认全推送

            if (ptr.Any())
                foreach(var i in ptr)
                    i.Value.DynamicInvoke(new object[] { args });
        }

    }
}
