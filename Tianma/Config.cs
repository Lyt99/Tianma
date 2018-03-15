using System;
using System.IO;
using System.Collections.Generic;

namespace Tianma
{
    /// <summary>
    /// 配置管理
    /// </summary>
    class Config
    {
        internal static readonly Config INSTANCE = new Config(Path.Combine(Globals.DATA_PATH, "config.cfg"));

        private string path;
        private Dictionary<string, string> configDic = new Dictionary<string, string>();

        private Config(string path)
        {
            this.path = path;
            //this.Refresh();
        }
    
        public void Refresh()
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (!line.Contains("=")) continue;

                var kv = Utils.Split2(line, '=');
                configDic.Add(kv[0], kv[1]);
            }

            sr.Close();
            fs.Close();
        }

        public string GetConfig(string key, string _default)
        {
            if (!this.configDic.ContainsKey(key))
                this.SetConfig(key, _default);

            return this.configDic[key];
        }

        public void SetConfig(string key, string value)
        {
            if (this.configDic.ContainsKey(key))
                this.configDic[key] = value;
            else
                this.configDic.Add(key, value);

            this.Save();
        }

        public void Save()
        {
            FileStream fs = new FileStream(this.path, FileMode.Open);
            StreamWriter sw = new StreamWriter(fs);

            foreach(var p in this.configDic)
            {
                sw.WriteLine(String.Format("{0}={1}", p.Key, p.Value));
            }

            sw.Close();
        }
    }
}
