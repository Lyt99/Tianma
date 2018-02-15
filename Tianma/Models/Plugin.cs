using System;
using System.Reflection;
using System.Linq;
using Tianma.API.Attributes;

namespace Tianma.Models
{
    /// <summary>
    /// 一个插件
    /// </summary>
    class Plugin
    {

        private object instance;
        private Assembly asm = null;
        private PluginMainAttribute attr = null;
        private string fileName;

        /// <summary>
        /// 创建一个插件并加载
        /// </summary>
        /// <param name="assembly">对应Assembly</param>
        /// <param name="filename">插件在目录中的文件名</param>
        public Plugin(Assembly assembly, string filename)
        {
            //只会取第一个搜索到的类
            this.fileName = filename;
            Type mainType = assembly.GetExportedTypes().Where((p) => p.GetCustomAttributes(typeof(PluginMainAttribute), false).Any()).FirstOrDefault();
            if (mainType != null)
            {
                instance = Activator.CreateInstance(mainType);
                attr = mainType.GetCustomAttributes(typeof(PluginMainAttribute), false).First() as PluginMainAttribute;
                asm = assembly;

                //注册事件
                EventManager.INSTANCE.RegisterAll(instance);
            }
            else
                throw new Exceptions.PluginLoadException(String.Format("在{0}中找不到特性中含有PluginMain的类", filename));
        }

        /// <summary>
        /// 插件是否加载成功
        /// </summary>
        public bool Load
        {
            get { return attr != null; }
        }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name
        {
            get { return attr.Name; }
        }

        /// <summary>
        /// 插件作者
        /// </summary>
        public string Author
        {
            get { return attr.Author; }
        }

        /// <summary>
        /// 插件说明
        /// </summary>
        public string Desc
        {
            get { return attr.Description; }
        }

        /// <summary>
        /// 插件对应的dll文件名
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
        }

        /// <summary>
        /// 插件的Assembly
        /// </summary>
        public Assembly PluginAssembly
        {
            get { return this.asm; }
        }

        /// <summary>
        /// 主类的实例
        /// </summary>
        public object Instance
        {
            get { return this.instance; }
        }
    }
}
