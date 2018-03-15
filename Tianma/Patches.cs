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
            ConnectionController.print(String.Format("Tianma {0} ready. {1} plugin(s) loaded.", Globals.VERSION, PluginManager.INSTANCE.PluginCount));
            ResCenter.NORMAL = Globals.CONFIG_MANAGER.GetConfig("antihexie", "true") == "true";

        }

        public static void WWW_Ctor_Prefix(ref string url, ref WWWForm form)
        {
            string prepared_url = Utils.PrepareURL(url);
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
                Url = prepared_url
            };

            EventManager.INSTANCE.InvokeEvent(API.Enums.EventType.WWWSend, prepared_url, res);
            WWWForm newform = new WWWForm();
            foreach (var i in res.Data)
            {
                newform.AddField(i.Key, i.Value);
            }
            form = newform;
            url = (url == prepared_url || res.Url.Contains("://")) ? res.Url : Utils.GetCurrentServer().addr + res.Url;
        }

        public static void WWW_Get_Text_Postfix(WWW __instance, ref string __result)
        {
            string url = Utils.PrepareURL(__instance.url);
            string data = Utils.AuthCodeWiseDecode(__result);
            bool encryptFlag = (data != __result);
            API.EventResults.EventWWWReceive res = new API.EventResults.EventWWWReceive()
            {
                Url = url,
                Data = data
            };

            EventManager.INSTANCE.InvokeEvent(API.Enums.EventType.WWWReceive, url, res);

            __result = encryptFlag ? Utils.AuthCodeEncode(res.Data) : res.Data;
        }
    }
}
