using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tianma.API.Attributes
{
    public class RegisterWWWReceive : RegisterEventAttribute
    {
        public string Url = null;

        public RegisterWWWReceive() : base(API.Enums.EventType.WWWReceive) { }

        public RegisterWWWReceive(string url) : base(API.Enums.EventType.WWWReceive) { this.Url = url; }

        public override string EventTag
        {
            get
            {
                return Url;
            }
        }
    }
}
