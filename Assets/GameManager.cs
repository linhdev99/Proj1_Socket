using System;
using System.Collections;
using System.Windows;
using System.Collections.Generic;
using System.Net; // để sử dụng lớp IPAddress, IPEndPoint
using System.Net.Sockets; // để sử dụng lớp Socket
using System.Text; // để sử dụng lớp Encoding
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class OptTransform
{
    public List<float> position { get; set; }
    public List<float> rotation { get; set; }
    public List<float> scale { get; set; }
}
class TemplateJsonObject
{
    public List<int> position;
    public List<int> rotation;
    public List<int> scale;
}
public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    private void Awake()
    {
        if (GM != null)
            GameObject.Destroy(gameObject);
        else
            GM = this;
        DontDestroyOnLoad(this);
    }
    string dataTest;
    public IPAddress serverIp;
    public int serverPort;
    IPEndPoint serverEndpoint;
    int size; // kích thước của bộ đệm
    byte[] receiveBuffer; // mảng byte làm bộ đệm            
    Socket socket;
    void Start()
    {
        ClientSocket();
    }

    void Update()
    {

    }
    public OptTransform GetDataServer()
    {
        OptTransform res = null;
        try
        {
            EndPoint dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
            int receiveBuf = socket.ReceiveFrom(receiveBuffer, ref dummyEndpoint);
            string recText = Encoding.ASCII.GetString(receiveBuffer, 0, receiveBuf);
            // Debug.Log("Receive: " + recText);

            // string to JObject
            Debug.Log(recText.Length);
            JObject json = JObject.Parse(recText);
            Debug.Log("1");
            res = json.ToObject<OptTransform>();
            print("2");
            Debug.Log("asd" + res.position[0]);

        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        return res;
        // Debug.Log("Convert from string to Json: " + json.ToString());

        // JObject to Object (type TemplateJsonObject)
        // TemplateJsonObject test = json.ToObject<TemplateJsonObject>();
        // Debug.Log("Convert from Json to Object : " + test.rotation[1]);

        // //Object (type TemplateJsonObject) to JObject
        // JObject obj2json = JObject.FromObject(test);
        // Debug.Log("Convert from Object to Json: " + obj2json.ToString());
        // string jsonString = "";
        // byte[] receiveBuffer = new byte[2048];
        // try
        // {
        //     int size = socket.Receive(receiveBuffer);
        //     jsonString = Encoding.ASCII.GetString(receiveBuffer, 0, size).Trim(' '); // byte --> string
        //     // Debug.Log(jsonString);
        // }
        // catch (System.Exception e)
        // {
        //     Debug.Log(e);
        // }
        // // Debug.Log(jsonString.Length);
        // return jsonString;
    }
    public void SetDataServer(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        OptTransform m_optTransform = new OptTransform
        {
            position = new List<float> { Mathf.Round(pos.x * 10000) / 10000, Mathf.Round(pos.y * 10000) / 10000, Mathf.Round(pos.z * 10000) / 10000 },
            rotation = new List<float> { Mathf.Round(rot.x * 10000) / 10000, Mathf.Round(rot.y * 10000) / 10000, Mathf.Round(rot.z * 10000) / 10000, Mathf.Round(rot.w * 10000) / 10000 },
            scale = new List<float> { Mathf.Round(scale.x * 10000) / 10000, Mathf.Round(scale.y * 10000) / 10000, Mathf.Round(scale.z * 10000) / 10000 },
            // position = new List<float> { pos.x, pos.y, pos.z },
            // rotation = new List<float> { rot.x, rot.y, rot.z, rot.w },
            // scale = new List<float> { scale.x, scale.y, scale.z },
        };
        // string jsonString = JsonConvert.SerializeObject(m_optTransform); // class --> string (json)
        // string text = "{\"position\":[0,0,0],\"rotation\":[1,1,1],\"scale\":[2,2,2]}";
        string jsonString = JObject.FromObject(m_optTransform).ToString();
        Debug.Log(jsonString);
        byte[] sendbuf = Encoding.ASCII.GetBytes(jsonString);
        try
        {
            socket.SendTo(sendbuf, serverEndpoint);
            Array.Clear(sendbuf, 0, sendbuf.Length);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        // byte[] sendBuffer = Encoding.ASCII.GetBytes(jsonString); // string --> byte
        // int size = sendBuffer.Length;
        // try
        // {
        //     socket.Send(sendBuffer, 0, size, 0);
        // }
        // catch (System.Exception e)
        // {
        //     Debug.Log(e);
        // }
        // dataTest = jsonString;
        // // Debug.Log(dataTest);
        // // socket.Close();
    }

    private void ClientSocket()
    {
        serverIp = IPAddress.Parse("127.0.0.1");
        serverPort = 1108;
        size = 1024;
        serverEndpoint = new IPEndPoint(serverIp, serverPort);
        receiveBuffer = new byte[size];
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // socket.Connect(serverEndpoint);
        // Debug.Log(socket.Connected);
        // string text = "{\"position\":[0,0,0],\"rotation\":[1,1,1],\"scale\":[2,2,2]}";
        // byte[] sendbuf = Encoding.ASCII.GetBytes(text);
        // socket.SendTo(sendbuf, serverEndpoint);
        // EndPoint dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
        // int receiveBuf = socket.ReceiveFrom(receiveBuffer, ref dummyEndpoint);
        // string recText = Encoding.ASCII.GetString(receiveBuffer, 0, receiveBuf);
        // Debug.Log("Receive: " + recText);

        // // string to JObject
        // JObject json = JObject.Parse(recText);
        // Debug.Log("Convert from string to Json: " + json.ToString());

        // // JObject to Object (type TemplateJsonObject)
        // TemplateJsonObject test = json.ToObject<TemplateJsonObject>();
        // Debug.Log("Convert from Json to Object : " + test.rotation[1]);

        // //Object (type TemplateJsonObject) to JObject
        // JObject obj2json = JObject.FromObject(test);
        // Debug.Log("Convert from Object to Json: " + obj2json.ToString());
    }
}
