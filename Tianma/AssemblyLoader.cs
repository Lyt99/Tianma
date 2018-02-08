using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace Tianma
{

    /// <summary>
    /// 插件加载器
    /// </summary>
    class AssemblyLoader
    {

        public Dictionary<Assembly, object> assemblies;

        public AssemblyLoader()
        {
            this.assemblies = new Dictionary<Assembly, object>();
        }


        private static byte[] ReadRawBytes(string path)
        {

            FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(input);
            byte[] byt = reader.ReadBytes((int)input.Length);
            reader.Close();
            input.Close();
            return byt;
        }


        public string LoadAssembly(string path)
        {
            try
            {
                Assembly ass = Assembly.Load(ReadRawBytes(path));
                //Assembly ass = Assembly.LoadFile(path);
                foreach (Type type in ass.GetExportedTypes())
                {
                    if(type.Name == "PluginMain")
                    {
                        var ins = Activator.CreateInstance(type);
                        assemblies.Add(ass, ins);
                        return ass.FullName;
                    }
                }

                Logger.Log(String.Format("{0} 未找到PluginMain类", ass.FullName));
                return "";
            }
            catch(Exception e)
            {
                Logger.Log(String.Format("{0} 错误:\n {1}", path, e.ToString()));
                return "";
            }
            
        }


        public object Invoke(string assemblyName, string methodName)
        {
            return Invoke(assemblyName, methodName, new object[0]);
        }

        public object Invoke(string assemblyName, string methodName, object[] args)
        {
            try {
                foreach (var pair in assemblies)
                {
                    if (pair.Key.FullName == assemblyName)
                    {
                        foreach (Type type in pair.Key.GetExportedTypes())
                        {
                            if (type.Name == "PluginMain")
                            {
                                MethodBase mb = type.GetMethod(methodName);
                                return mb.Invoke(pair.Value, args);
                            }
                        }
                    }
                }
                return null;
            }
            catch(NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }
    }
}
