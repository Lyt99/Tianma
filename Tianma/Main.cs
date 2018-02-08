using System;
using UnityEngine;
using Harmony;
using System.Reflection;

namespace Tianma
{
    public class Main
    {
        private static Harmony.HarmonyInstance harmony_ins = HarmonyInstance.Create("pw.baka.tianma.core");

        public static void Unity_Debug_Log_Entry(object message)
        {
            //LoginNoticeController.Awake()，Debug.Log会输出 初始化SDK + AndroidID
            if(message.ToString().Contains("初始化SDK"))
            {
                Debug.Log("Initializing Tianma...");
                //初始化，Hook几个关键方法
                //AccessTool真好用
                //UserCenter.Init(string key, string appId)
                MethodInfo method_init = AccessTools.Method(typeof(UserCenter), "Init");
                MethodInfo method_init_prefix = AccessTools.Method(typeof(Patches), "MainEntry");
                harmony_ins.Patch(method_init, new HarmonyMethod(method_init_prefix), null);

                //ConnectionController.EnqueueAndRequest(Request request = null)
                MethodInfo method_ear = AccessTools.Method(typeof(ConnectionController), "EnqueueAndRequest");
                MethodInfo method_ear_prefix = AccessTools.Method(typeof(Patches), "EnqueueAndRequest_Prefix");
                harmony_ins.Patch(method_ear, new HarmonyMethod(method_ear_prefix), null);

                Debug.Log("Patching OK!");
            }
        }
    }
}
