using System.Text;
using System.IO;
using System.Diagnostics;
using System;

namespace Tianma
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger
    {

        private static readonly string logpath =  Config.DATA_PATH + @"/log.txt";

        /// <summary>
        /// 记录日志信息至Log.txt
        /// </summary>
        /// <param name="str">信息</param>
        public static void Log(object str)
        {
            FileStream fs = new FileStream(logpath, FileMode.Append);
            var w = Encoding.Default.GetBytes(str.ToString() + '\n');
            fs.Write(w, 0, w.Length);
            fs.Close();
        }

        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="e">错误</param>
        public static void LogError(Exception err)
        {
            StackTrace st = new StackTrace();
            string errorMethod = st.GetFrames()[2].GetMethod().Name;
            StringBuilder errsb = new StringBuilder();
            errsb.Append("**************Tianma 运行错误***************\n");
            errsb.Append("错误(可能)产生于: " + errorMethod);
            errsb.Append('\n');
            errsb.Append("错误详细信息:\n" + err.ToString());
            errsb.Append('\n');
            errsb.Append("******************************************\n");
            Log(errsb.ToString());
        }
    }
}
