using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tianma.API.Attributes
{
    public class RegisterWWWSend : RegisterEventAttribute
    {
        public string Url = null;

        public RegisterWWWSend() : base(API.Enums.EventType.WWWSend) { }

        public RegisterWWWSend(string url) : base(API.Enums.EventType.WWWSend) { this.Url = url; }

        public override string EventTag
        {
            get
            {
                return Url;
            }
        }

    }
}
