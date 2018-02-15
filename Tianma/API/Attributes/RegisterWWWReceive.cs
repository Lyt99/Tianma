using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tianma.API.Attributes
{
    class RegisterWWWReceive : RegisterEventAttribute
    {
        public string Url;

        public RegisterWWWReceive() : base(API.Enums.EventType.WWWReceive) { }

        public override bool Check(MethodInfo method)
        {
            if (String.IsNullOrEmpty(this.Url)) return false;
            return true;
        }

        public override string EventType
        {
            get
            {
                return EventType + Url;
            }
        }
    }
}
