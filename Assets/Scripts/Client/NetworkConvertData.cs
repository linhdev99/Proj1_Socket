using System.Collections;
using System.Collections.Generic;
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
}
