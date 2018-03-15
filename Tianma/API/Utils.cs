using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tianma.API
{
    /// <summary>
    /// 开放给外部使用的Utils类
    /// </summary>
    public class Utils
    {

        /// <summary>
        /// 获得Tianma程序运行路径
        /// </summary>
        /// <returns></returns>
        public static string GetTianmaPath()
        {
            return Tianma.Utils.GetTianmaPath();
        }

        /// <summary>
        /// 游戏中弹出提示框
        /// </summary>
        /// <param name="message">信息</param>
        public static void MessageBox(string message)
        {
            Tianma.Utils.MessageBox(message);
        }

        /// <summary>
        /// 对加密数据进行解密(key为当前用户sign)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string AuthCodeDecode(string text)
        {
            return Tianma.Utils.AuthCodeDecode(text);
        }

        /// <summary>
        /// 对加密数据进行加密(key为当前用户sign)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string AuthCodeEncode(string text)
        {
            return Tianma.Utils.AuthCodeEncode(text);
        }
    }
}
