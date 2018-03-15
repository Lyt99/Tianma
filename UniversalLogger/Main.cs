using System;
using Tianma.API.Attributes;

namespace UniversalLogger
{
    [PluginMain("UniversalLogger", "Lyt99", "Log everything.")]
    public class Main
    {
        public Main()
        {
            Tianma.API.Logger.Log("Logger Initalized.");
        }

        [RegisterWWWReceive]
        public void WWWReceive(Tianma.API.EventResults.EventWWWReceive eventWWWReceive)
        {
            Tianma.API.Logger.Log(String.Format("Receive API:{0}\n{1}\n", eventWWWReceive.Url, eventWWWReceive.Data.Length > 200 ? "(ignored)" : eventWWWReceive.Data));
        }

        [RegisterWWWSend]
        public void WWWSend(Tianma.API.EventResults.EventWWWSend eventWWWSend)
        {
            Tianma.API.Logger.Log(String.Format("Send API:{0}\n{1}\n", eventWWWSend.Url, eventWWWSend.Data.Keys));
        }
    }
}
