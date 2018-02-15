using System;
using UnityEngine;
using Harmony;
using System.Reflection;

namespace Tianma
{
    public class Main
    {
        private static HarmonyInstance harmonyIns = HarmonyInstance.Create("pw.baka.tianma.core");

        public static void Unity_Debug_Log_Entry(object message)
        {
            //LoginNoticeController.Awake()，Debug.Log会输出 初始化SDK + AndroidID
            if(message.ToString().Contains("初始化SDK"))
            {
                Debug.Log("Initializing Tianma...");
                //初始化，Hook几个关键方法
                //不过有点丑，尝试更优雅的方法
                //AccessTool真好用
                //UserCenter.Init(string key, string appId)
                Debug.Log("Patching Main Entry...");
                MethodInfo method_init = AccessTools.Method(typeof(UserCenter), "Init");
                MethodInfo method_init_prefix = AccessTools.Method(typeof(Tianma.Patches), "MainEntry");
                harmonyIns.Patch(method_init, new HarmonyMethod(method_init_prefix), null);

                //UnityEngine.WWW .ctor(string url, UnityEngine.WWWForm form)
                Debug.Log("Patching WWW_Ctor_Prefix...");
                ConstructorInfo method_WWW_ctor = AccessTools.Constructor(typeof(WWW), new Type[] { typeof(string), typeof(WWWForm) });
                MethodInfo method_WWW_ctor_prefix = AccessTools.Method(typeof(Tianma.Patches), "WWW_Ctor_Prefix");
                harmonyIns.Patch(method_WWW_ctor, new HarmonyMethod(method_WWW_ctor_prefix), null);

                //UnityEngine.WWW get_text()
                Debug.Log("Patching WWW_Get_Text_Postfix...");
                MethodInfo method_WWW_get_text = AccessTools.Method(typeof(WWW), "get_text");
                MethodInfo method_WWW_get_text_postfix = AccessTools.Method(typeof(Tianma.Patches), "WWW_Get_Text_Postfix");
                harmonyIns.Patch(method_WWW_get_text, null, new HarmonyMethod(method_WWW_get_text_postfix));

                Debug.Log("Patching Done!");
            }
        }
    }
}
