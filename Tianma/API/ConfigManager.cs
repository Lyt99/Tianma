using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tianma.API
{
    public class ConfigManager
    {

        private string prefix;

        /// <summary>
        /// 配置文件管理器
        /// </summary>
        /// <param name="configPrefix">你的配置前缀，推荐使用插件名</param>
        public ConfigManager(string configPrefix)
        {
            if (String.IsNullOrEmpty(configPrefix)) throw new Exceptions.TianmaException("配置前缀不能为空！");
            this.prefix = configPrefix + ":";
        }

        public string GetConfig(string key, string _default)
        {
            return Config.INSTANCE.GetConfig(prefix + key, _default);
        }

        public void SetConfig(string key, string value)
        {
            Config.INSTANCE.SetConfig(prefix + key, value);
        }
    }
}
