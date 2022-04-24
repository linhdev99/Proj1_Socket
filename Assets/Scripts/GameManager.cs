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
        ConnectClientSocket();
    }

    void Update()
    {

    }
    public OptTransform ReceiveDataFormServer()
    {
        OptTransform res = null;
        try
        {
            EndPoint dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
            int receiveBuf = socket.ReceiveFrom(receiveBuffer, ref dummyEndpoint);
            string recText = Encoding.ASCII.GetString(receiveBuffer, 0, receiveBuf);
            JObject json = JObject.Parse(recText);
            res = json.ToObject<OptTransform>();

        }
        catch (System.Exception e)
        {
            Debug.LogErrorFormat("Lỗi: Chỗ ReceiveDataFormServer \n {0}", e.Message);
            ConnectClientSocket();
        }
        return res;
    }
    public void SendDataToServer(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        // Debug.Log(socket.);
        // return;
        try
        {
            OptTransform m_optTransform = new OptTransform
            {
                position = new List<float> { Mathf.Round(pos.x * 10000) / 10000, Mathf.Round(pos.y * 10000) / 10000, Mathf.Round(pos.z * 10000) / 10000 },
                rotation = new List<float> { Mathf.Round(rot.x * 10000) / 10000, Mathf.Round(rot.y * 10000) / 10000, Mathf.Round(rot.z * 10000) / 10000, Mathf.Round(rot.w * 10000) / 10000 },
                scale = new List<float> { Mathf.Round(scale.x * 10000) / 10000, Mathf.Round(scale.y * 10000) / 10000, Mathf.Round(scale.z * 10000) / 10000 },
            };
            string jsonString = JObject.FromObject(m_optTransform).ToString();
            byte[] sendbuf = Encoding.ASCII.GetBytes(jsonString);
            socket.SendTo(sendbuf, serverEndpoint);
            Array.Clear(sendbuf, 0, sendbuf.Length);
        }
        catch (System.Exception e)
        {
            Debug.LogErrorFormat("Lỗi: Chỗ SendDataToServer \n {0}", e.Message);
        }
    }

    private void ConnectClientSocket()
    {
        try
        {
            serverIp = IPAddress.Parse("127.0.0.1");
            serverPort = 1108;
            size = 1024;
            serverEndpoint = new IPEndPoint(serverIp, serverPort);
            receiveBuffer = new byte[size];
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SendTimeout = 1000;
            socket.ReceiveTimeout = 1000;
        }
        catch (System.Exception e)
        {
            Debug.LogErrorFormat("Lỗi: Chỗ ConnectClientSocket \n {0}", e.Message);
        }
    }
}
