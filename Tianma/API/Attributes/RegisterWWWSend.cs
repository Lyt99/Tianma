using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tianma.API.Attributes
{
    class RegisterWWWSend : RegisterEventAttribute
    {
        public string Url;

        public RegisterWWWSend() : base(API.Enums.EventType.WWWSend) { }

        public override bool Check(MethodInfo method)
        {
            if (String.IsNullOrEmpty(Url)) return false;
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
