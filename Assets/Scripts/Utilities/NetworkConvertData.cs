using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
namespace BKSpeed
{

    public class NetworkConvertData
    {
        public string Convert_ChatMessage_To_JsonString(ChatMessage data)
        {
            string jsonString = JObject.FromObject(data).ToString();
            return jsonString;
        }
        public string Convert_MessageSocket_To_JsonString(MessageSocket data)
        {
            string jsonString = JObject.FromObject(data).ToString();
            return jsonString;
        }
        public MessageSocket Convert_JsonString_To_MessageSocket(string rawString)
        {
            if (rawString == "" || rawString == null)
            {
                return null;
            }
            JObject json = JObject.Parse(rawString);
            MessageSocket result = json.ToObject<MessageSocket>();
            Dictionary<string, string> test = JObject.Parse(result.getValue()).ToObject<Dictionary<string, string>>();
            return result;
        }
        public string Convert_Room_To_JsonString(Room data)
        {
            string jsonString = JObject.FromObject(data).ToString();
            return jsonString;
        }
        public string Convert_UserDataRealtime_To_JsonString(UserDataRealtime data)
        {
            string jsonString = JObject.FromObject(data).ToString();
            return jsonString;
        }
        public Dictionary<string, UserDataRealtime> Convert_JsonString_To_UserDataRealtime(string rawString)
        {
            if (rawString == "" || rawString == null)
            {
                return null;
            }
            Dictionary<string, UserDataRealtime> result = new Dictionary<string, UserDataRealtime>();
            Dictionary<string, JObject> data = new Dictionary<string, JObject>();
            JObject json = JObject.Parse(rawString);
            data = json.ToObject<Dictionary<string, JObject>>();
            foreach (string key in data.Keys)
            {
                result.Add(key, data[key].ToObject<UserDataRealtime>());
            }
            return result;
        }
        public Dictionary<string, int> Convert_JsonString_To_DictInt(string rawString)
        {
            if (rawString == "" || rawString == null)
            {
                return null;
            }
            Dictionary<string, int> result = new Dictionary<string, int>();
            JObject json = JObject.Parse(rawString);
            result = json.ToObject<Dictionary<string, int>>();
            return result;
        }
    }
}
