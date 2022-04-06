using System;
using System.Collections;
using System.Windows;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class OptTransform
{
    public List<float> position { get; set; }
    public List<float> rotation { get; set; }
    public List<float> scale { get; set; }
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
    List<byte> receiveBuffer; // mảng byte làm bộ đệm            
    Socket socket;
    void Start()
    {
        ClientSocket();
    }

    void Update()
    {

    }
    public string GetDataServer()
    {
        string jsonData = "";
        byte[] receiveBuffer = new byte[2048];
        try
        {
            int size = socket.Receive(receiveBuffer);
            jsonData = Encoding.ASCII.GetString(receiveBuffer, 0, size);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        return jsonData;
    }
    public void SetDataServer(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        OptTransform m_optTransform = new OptTransform
        {
            position = new List<float> { pos.x, pos.y, pos.z },
            rotation = new List<float> { rot.x, rot.y, rot.z, rot.w },
            scale = new List<float> { scale.x, scale.y, scale.z },
        };
        string jsonString = JsonConvert.SerializeObject(m_optTransform);
        byte[] sendBuffer = Encoding.ASCII.GetBytes(jsonString);
        int size = sendBuffer.Length;
        try
        {
            socket.Send(sendBuffer, 0, size, 0);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        // socket.Close();
    }

    private void ClientSocket()
    {
        serverIp = IPAddress.Parse("127.0.0.1");
        serverPort = 1108;
        serverEndpoint = new IPEndPoint(serverIp, serverPort);
        size = 4096;
        receiveBuffer = new List<byte>();
        socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        socket.Connect(serverEndpoint);
        Debug.Log(socket.Connected);
    }
}
