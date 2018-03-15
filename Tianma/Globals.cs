using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tianma
{
    class Globals
    {
        public static string VERSION = "0.0.1";
        public static string DATA_PATH = "/storage/emulated/0/Tianma";
        public static API.ConfigManager CONFIG_MANAGER = new API.ConfigManager("Tianma");
    }
}
