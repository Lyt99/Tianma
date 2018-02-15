using System;
using System.Collections.Generic;
using Tianma.Models;
using System.IO;
using System.Reflection;

namespace Tianma
{

    /// <summary>
    /// 插件管理器
    /// </summary>
    class PluginManager
    {
        public static readonly PluginManager INSTANCE = new PluginManager(Config.DATA_PATH);

        private DirectoryInfo dirInfo;
        private List<Plugin> pluginList = new List<Plugin>();

        public PluginManager(string pluginPath)
        {
            dirInfo = new DirectoryInfo(pluginPath);
        }

        /// <summary>
        /// 刷新插件
        /// </summary>
        public void Refersh()
        {
            if (!this.dirInfo.Exists) //不存在即创建
            {
                this.dirInfo.Create();
                this.dirInfo.Refresh();
            }
            else
            {
                foreach (FileInfo fi in dirInfo.GetFiles("*.dll"))
                {
                    try
                    {
                        Assembly asm = AssemblyLoader.LoadAssembly(fi.FullName);
                        if (asm == null) throw new Exceptions.PluginLoadException(String.Format("无法加载 {0} 为Assembly", fi.Name));
                        Plugin plugin = new Plugin(asm, fi.Name);
                        this.pluginList.Add(plugin);
                    }
                    catch (Exception e)
                    {
                        Logger.LogError(e);
                    }
                }
            }
        }

        /// <summary>
        /// 插件文件夹地址
        /// </summary>
        public string Path
        {
            get { return this.dirInfo.FullName; }
        }

        /// <summary>
        /// 已加载的插件数量
        /// </summary>
        public int PluginCount
        {
            get { return this.pluginList.Count; }
        }

        /// <summary>
        /// 插件列表
        /// </summary>
        public List<Plugin> PluginList
        {
            get { return this.pluginList; }
        } 

    }
}
