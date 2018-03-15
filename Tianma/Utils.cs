using AC;
using ICSharpCode.SharpZipLib.GZip;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Tianma
{
    class Utils
    {
        /// <summary>
        /// 获得游戏数据存储路径
        /// </summary>
        /// <returns>路径</returns>
        public static string GetApplicationDataPath()
        {
            return Application.dataPath;
        }

        /// <summary>
        /// 获得Tianma数据存储路径
        /// </summary>
        /// <returns>路径</returns>
        public static string GetTianmaPath()
        {
            return Globals.DATA_PATH;
        }

        /// <summary>
        /// 弹出游戏内信息框(CommonController.MessageBox())
        /// </summary>
        /// <param name="str">文本</param>
        public static void MessageBox(string str)
        {
            CommonController.MessageBox(str);
        }

        /// <summary>
        /// 获得Assembly-CSharp.dll的System.Reflection.Assembly对象，用于进行反射等操作
        /// </summary>
        /// <returns>Assembly</returns>
        public static Assembly GetAssemblyCsharpAssembly()
        {
            //这样会不会有一种钦点的感觉
            return typeof(ConnectionController).Assembly;
        }

        /// <summary>
        /// 对加密数据进行解密
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string AuthCodeDecode(string text, string key)
        {
            return AC.AuthCode.Decode(text, key);
        }

        /// <summary>
        /// 对加密数据进行解密(key为当前用户sign)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string AuthCodeDecode(string text)
        {
            string key = ConnectionController.sign;
            if (string.IsNullOrEmpty(key)) return string.Empty;
            return AuthCodeDecode(text, key);
        }

        /// <summary>
        /// 对数据进行加密(时间戳可能不对什么的不要在意)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="key">key</param>
        /// <returns>解密后文本</returns>
        public static string AuthCodeEncode(string text, string key)
        {
            return AC.AuthCode.Encode(text, key);
        }

        /// <summary>
        /// 对数据进行加密(时间戳可能不对什么的不要在意)
        /// key为当前sign
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>解密后文本</returns>
        public static string AuthCodeEncode(string text)
        {
            string key = ConnectionController.sign;
            if (string.IsNullOrEmpty(key)) return string.Empty;
            return AuthCodeEncode(text, key);
        }

        /// <summary>
        /// 使用Gzip模式解密数据
        /// 和单纯的AC.DecodeWithGzip()不一样，会帮你将数据解压
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>解密后文本</returns>
        public static string AuthCodeDecodeWithGzip(string text)
        {
            string key = ConnectionController.sign;
            if (string.IsNullOrEmpty(key)) return string.Empty;
            using (MemoryStream memoryStream = new MemoryStream(AuthCode.DecodeWithGzip(text.Substring(1), key)))
            {
                using (Stream stream = new GZipInputStream(memoryStream))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.Default))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// 自动判断是哪种加密模式并解密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string AuthCodeWiseDecode(string text)
        {
            string result = string.Empty;
            if (text.StartsWith("#")) result = AuthCodeDecodeWithGzip(text);
            else result = AuthCodeDecode(text);

            if (string.IsNullOrEmpty(result)) return text;
            else return result;
        }

        /// <summary>
        /// 将可以剪切去掉服务器地址的URL去除地址
        /// </summary>
        /// <param name="url">原URL</param>
        /// <returns>处理之后的URL</returns>
        public static string PrepareURL(string url)
        {

            if (ConnectionController.currentServer != null && !string.IsNullOrEmpty(ConnectionController.currentServer.addr))
            {
                string u = ConnectionController.currentServer.addr;
                if (url.Length > u.Length && url.Contains(u))
                    return url.Substring(u.Length);
                else
                    return url;
            }
            else
                return url;
        }

        /// <summary>
        /// 将一个文本按某个字符分割成最多两份
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string[] Split2(string str, char sp)
        {
            string[] s = str.Split(sp);
            if (s.Length <= 2) return s;
            else
            {
                return new string[] { s[0], String.Join("", s, 1, s.Length - 1)};
            }

        } 

        /// <summary>
        /// 获得当前所处服务器
        /// </summary>
        /// <returns>服务器</returns>
        public static Server GetCurrentServer()
        {
            return ConnectionController.currentServer;
        }
    }
}
