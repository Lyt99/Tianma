using System;
using System.IO;
using System.Reflection;

namespace Tianma
{

    /// <summary>
    /// Assembly加载器
    /// </summary>
    class AssemblyLoader
    {
        private static byte[] GetRawBytes(string file)
        {

            FileStream input = new FileStream(file, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(input);
            byte[] byt = reader.ReadBytes((int)input.Length);
            reader.Close();
            input.Close();
            return byt;
        }

        /// <summary>
        /// 加载Assembly并返回，加载失败返回null
        /// </summary>
        /// <param name="file">文件名(带路径)</param>
        /// <returns></returns>
        public static Assembly LoadAssembly(string file)
        {
            try
            {
                return Assembly.Load(GetRawBytes(file));
            }
            catch(Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }
    }
}
