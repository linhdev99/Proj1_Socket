using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class NetworkConvertData
{
    public string ConvertTransformGOToJsonString(TransformGO transGO)
    {
        string jsonString = JObject.FromObject(transGO).ToString();
        return jsonString;
    }
    public TransformGO ConvertJsonStringToTransformGO(string rawString)
    {
        TransformGO transGO = null;
        JObject json = JObject.Parse(rawString);
        transGO = json.ToObject<TransformGO>();
        return transGO;
    }
    public string ConvertNetworkClientDataToJsonString(NetworkClientData data)
    {
        string jsonString = JObject.FromObject(data).ToString();
        return jsonString;
    }
    public Dictionary<string, NetworkClientData> ConvertJsonStringToNetworkClientData(string rawString)
    {
        if (rawString == "" || rawString == null)
        {
            return null;
        }
        Dictionary<string, NetworkClientData> result = new Dictionary<string, NetworkClientData>();
        Dictionary<string, JObject> data = new Dictionary<string, JObject>();
        JObject json = JObject.Parse(rawString);
        data = json.ToObject<Dictionary<string, JObject>>();
        foreach (string key in data.Keys)
        {
            result.Add(key, data[key].ToObject<NetworkClientData>());
        }
        return result;
    }
}
