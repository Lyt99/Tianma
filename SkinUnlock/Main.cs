using System;
using System.Collections.Generic;
using System.Text;
using Tianma.API.Attributes;
using Tianma.API.EventResults;
using Tianma.API;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SkinUnlock
{
    [PluginMain("SkinUnlock", "Lyt99", "0.0.1", Description = "Unlock your waifu")]
    public class Main
    {
        private ConfigManager cm = new ConfigManager("SkinUnlock");

        private Dictionary<string, string> skinDict = new Dictionary<string, string>();
        private string adjutant = null;

        public Main()
        {
            adjutant = cm.GetConfig("adjutant", "0:0:0");
            string recs = cm.GetConfig("skins", "");
            if(!String.IsNullOrEmpty(recs))
            {
                var skins = recs.Split(',');
                foreach(string s in skins)
                {
                    var i = s.Split(':');
                    if (i.Length == 2)
                        skinDict.Add(i[0], i[1]);
                }
            }
        }

        public void UpdateSkins()
        {
            var l = from i in this.skinDict select String.Format("{0}:{1}", i.Key, i.Value);
            cm.SetConfig("skins", String.Join(",", l.ToArray()));
        }

        [RegisterWWWReceive(Tianma.API.Enums.RequestUrls.GetUserInfo)]
        public void UserIndexReceived(EventWWWReceive eventWWWReceive)
        {
            string json_text = eventWWWReceive.Data;

            var obj = LitJson.JsonMapper.ToObject(json_text);
            int id = obj["user_info"]["user_id"].Int;

            var listSkin = new LitJson.JsonData();

            foreach (var i in GameData.listSkinInfo.GetList())
            {
                LitJson.JsonData d = new LitJson.JsonData
                {
                    ["skin_id"] = i.id.ToString(),
                    ["user_id"] = id
                };

                d["user_id"].SetJsonType(LitJson.JsonType.Int);

                listSkin[i.id.ToString()] = d;
            }

            obj["skin_with_user_info"] = listSkin;

            if (adjutant != "0,0,0") obj["user_record"]["adjutant"] = adjutant;

            foreach (LitJson.JsonData i in obj["gun_with_user_info"])
            {
                if (!i.Contains("id")) continue;
                if (skinDict.ContainsKey(i["id"].String))
                    i["skin"] = skinDict[i["id"].String];
            }

            eventWWWReceive.Data = obj.ToJson();
        }

        [RegisterWWWSend(Tianma.API.Enums.RequestUrls.ChangeCloth)]
        public void ChangSkinSend(EventWWWSend eventWWWSend)
        {
            string json_text = Utils.AuthCodeDecode(eventWWWSend.Data["outdatacode"]);
            var obj = LitJson.JsonMapper.ToObject(json_text);

            string id = obj["gun_with_user_id"].String, skin = obj["skin_id"].String;
            if (skinDict.ContainsKey(id))
                skinDict[id] = skin;
            else
                skinDict.Add(id, skin);

            UpdateSkins();
            obj["skin_id"] = 0;
            eventWWWSend.Data["outdatacode"] = Utils.AuthCodeEncode(obj.ToJson());
        }

        [RegisterWWWSend(Tianma.API.Enums.RequestUrls.ChangeAdjutant)]
        public void ChangeAdjutant(EventWWWSend eventWWWSend)
        {
            string json_text = Utils.AuthCodeDecode(eventWWWSend.Data["outdatacode"]);
            var obj = LitJson.JsonMapper.ToObject(json_text);
            adjutant = String.Format("{0},{1},{2}", obj["gun_id"].String, obj["skin_id"].String, obj["is_sexy"].String);
            obj["skin_id"] = 0;
            cm.SetConfig("adjutant", adjutant);
            eventWWWSend.Data["outdatacode"] = Utils.AuthCodeEncode(obj.ToJson());
        }
    }
}
