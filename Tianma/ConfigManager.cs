using System.IO;

namespace Tianma
{
    /// <summary>
    /// 配置管理
    /// </summary>
    class ConfigManager
    {
        public static readonly ConfigManager INSTANCE = new ConfigManager(Path.Combine(Config.DATA_PATH, "config.cfg"));


        private ConfigManager(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            
        }
    
    }
}
