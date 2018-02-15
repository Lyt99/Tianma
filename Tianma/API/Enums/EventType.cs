using System;
namespace Tianma.API.Enums
{
    class EventType
    {
        /// <summary>
        /// 向服务器发送请求
        /// </summary>
        public static readonly string WWWSend = "WWWSend";

        /// <summary>
        /// 获得服务器请求结果
        /// </summary>
        public static readonly string WWWReceive = "WWWReceive";
    }
}