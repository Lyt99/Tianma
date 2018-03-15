using System.Text;
using System.IO;
using UnityEngine;
using System;

namespace Tianma.API
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger
    {

        private static readonly string logPath =  Config.DATA_PATH + @"/log.txt";

        /// <summary>
        /// 记录日志信息至数据目录下的log.txt
        /// </summary>
        /// <param name="value">信息</param>
        public static void Log(object value)
        {
            Debug.Log(value);
            FileStream fs = new FileStream(logPath, FileMode.Append);
            var w = Encoding.Default.GetBytes(value.ToString() + '\n');
            fs.Write(w, 0, w.Length);
            fs.Close();
        }

        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="e">错误</param>
        public static void LogError(Exception err)
        {
            StringBuilder errsb = new StringBuilder();
            errsb.Append("************** Tianma 运行错误***************\n");
            errsb.Append("StackTrace:\n");
            errsb.Append(err.StackTrace);
            errsb.Append('\n');
            errsb.Append("错误详细信息:\n" + err.ToString());
            errsb.Append('\n');
            errsb.Append("******************************************\n");
            Log(errsb.ToString());
        }
    }
}
