using System;
using System.Reflection;

namespace Tianma.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    class RegisterEventAttribute : Attribute
    {

        private string eventType;

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name=""></param>
        public RegisterEventAttribute(string eventType)
        {
            this.eventType = eventType;
        }

        /// <summary>
        /// 检查该方法是否合法
        /// </summary>
        /// <returns></returns>
        public virtual bool Check(MethodInfo method)
        {
            return true;
        }

        public virtual string EventType
        {
            get
            {
                return this.eventType;
            }
        }
    }
}