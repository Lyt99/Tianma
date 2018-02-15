using System;

namespace Tianma.API.Attributes
{

    /// <summary>
    /// 插件主类和入口点，会在加载后创建一个实例
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    class PluginMainAttribute : Attribute
    {
        private string name;
        private string author;
        private string version;
        private string desc = String.Empty;

        public PluginMainAttribute(string name, string author, string version)
        {
            this.name = name;
            this.author = author;
            this.version = version;
        }

        public string Name { get { return this.name; } }
        public string Author { get { return this.author; } }
        public string Version { get { return this.version; } }
        public string Description
        {
            get { return this.desc; }
            set { this.desc = value; }
        }

    }
}
