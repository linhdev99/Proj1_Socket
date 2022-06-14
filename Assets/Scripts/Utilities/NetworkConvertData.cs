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
            // Dictionary<string, string> aaa = JsonUtility.FromJson<Dictionary<string, string>>(rawString);
            // foreach (string key in aaa.Keys)
            // {
            //     Debug.Log(aaa[key]);
            // }

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
    }
}