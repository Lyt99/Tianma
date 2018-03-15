
namespace Tianma
{
    class Globals
    {
        public static string VERSION = "0.0.1";
        public static string DATA_PATH = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "Tianma/");
        public static API.ConfigManager CONFIG_MANAGER = new API.ConfigManager("Tianma");
    }
}
