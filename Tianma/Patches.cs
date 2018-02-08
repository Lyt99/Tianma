using System;
using UnityEngine;
using LitJson;

namespace Tianma
{
    class Patches
    {
        public static void MainEntry()
        {
            //入口，负责插件的加载和初始化
            ConnectionController.print(String.Format("Tianma {0} ready.", Config.VERSION));
            Debug.Log("Prefix OK!");
        }

        public static void EnqueueAndRequest_Prefix(Request request)
        {
            //请求入队列
            if (request == null) return;
            Debug.Log(String.Format("Receive request: {0}", request.functionName));
        }

        public static void MapAndDecodeJson_Postfix(string wwwText, JsonData __result)
        {
            //不过这样不太好，拿不到请求的地址了，暂留
        }
    }
}
