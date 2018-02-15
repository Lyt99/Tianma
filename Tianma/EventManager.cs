using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Tianma
{
    /// <summary>
    /// 注册、分发、管理事件
    /// </summary>
    class EventManager
    {
        //但是这样就只能用MethodBase注册事件了，尬的要死

        public static readonly EventManager INSTANCE = new EventManager();

        private Dictionary<string, Delegate> events = new Dictionary<string, Delegate>();


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
                    this.RegisterEvent(attr.EventType, instance, i);
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
        public bool RegisterEvent(string eventType, Action method)
        {
            return this.RegisterEvent(eventType, (Delegate)method);
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="method">方法</param>
        /// <returns></returns>
        public bool RegisterEvent(string eventType, Delegate method)
        {
            try
            {
                if (events.ContainsKey(eventType))
                {
                    events[eventType] = Delegate.Combine(events[eventType], method);
                }
                else
                {
                    events.Add(eventType, method);
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
        public bool RegisterEvent(string eventType, object classInstance, MethodInfo method)
        {
            try
            {
                Delegate @delegate = Delegate.CreateDelegate(typeof(Delegate), classInstance, method);
                return this.RegisterEvent(eventType, @delegate);
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
        public void InvokeEvent(string eventType, params object[] args)
        {
            //如果存在再推送
            if(events.ContainsKey(eventType))
                events[eventType].DynamicInvoke(args);
        }

    }
}
