using System;
namespace Tianma.API.Enums
{
    public class EventType
    {
        /// <summary>
        /// 向服务器发送请求
        /// </summary>
        public const string WWWSend = "WWWSend";

        /// <summary>
        /// 获得服务器请求结果
        /// </summary>
        public const string WWWReceive = "WWWReceive";
    }
}