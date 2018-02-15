using System;
using UnityEngine;
using System.IO;
using LitJson;
using System.Collections.Generic;
using System.Text;

namespace Tianma
{
    public class Patches
    {
        public static void MainEntry()
        {
            //入口，负责插件的加载和初始化
            PluginManager.INSTANCE.Refersh(); //在这里加载插件
            ConnectionController.print(String.Format("Tianma {0} ready. {1} plugin(s) loaded.", Config.VERSION, PluginManager.INSTANCE.PluginCount));

        }

        public static void WWW_Ctor_Prefix(string url, ref WWWForm form)
        {
            url = Utils.PrepareURL(url);
            Dictionary<string, string> data = new Dictionary<string, string>();
            var d = Encoding.Default.GetString(form.data).Split('&');
            foreach (var i in d)
            {
                int pos = i.IndexOf('=');
                if (!data.ContainsKey(i.Substring(0, pos)))
                    data.Add(i.Substring(0, pos), Uri.UnescapeDataString(i.Substring(pos + 1)));
            }
            API.EventResults.EventWWWSend res = new API.EventResults.EventWWWSend
            {
                Data = data,
                Url = url
            };

            EventManager.INSTANCE.InvokeEvent(API.Enums.EventType.WWWSend + url, res);
            WWWForm newform = new WWWForm();
            foreach (var i in data)
            {
                newform.AddField(i.Key, i.Value);
            }
            form = newform;
        }

        public static void WWW_Get_Text_Postfix(WWW __instance, ref string __result)
        {
            string url = Utils.PrepareURL(__instance.url);
            string data = Utils.AuthCodeWiseDecode(__result);
            bool encryptFlag = (data == __result);
            API.EventResults.EvnetWWWReceive res = new API.EventResults.EvnetWWWReceive()
            {
                Url = url,
                Data = data
            };

            EventManager.INSTANCE.InvokeEvent(API.Enums.EventType.WWWReceive + url, res);

            __result = encryptFlag ? Utils.AuthCodeEncode(res.Data) : res.Data;
        }
    }
}
