using System;
using Tianma.API.Attributes;
using Tianma.API.EventResults;

namespace iOSServer
{
    [PluginMain("iOSServer", "Lyt99", "0.1", Description = "Change server to iOS")]
    public class PluginMain
    {
        public PluginMain()
        {

        }

        [RegisterWWWSend("http://adr.transit.gf.ppgame.com/index.php")]
        public void ChangeServer(EventWWWSend eventWWWReceive)
        {
            eventWWWReceive.Url = "http://ios.transit.gf.ppgame.com/index.php";
        }
    }
}
