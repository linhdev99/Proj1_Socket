using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager NWManager;
    string serverIP = "127.0.0.1";
    int port = 8080;
    #region "Public Members"
    public string id;
    public NetworkRoom networkRoom;
    public string receiveData;
    public bool canReceiveData;
    public string sendData;
    #endregion

    #region "Private Members"
    Socket udp;
    IPEndPoint endPoint;
    EndPoint dummyEndpoint;
    Coroutine coroutineReceivePacket;
    Coroutine coroutineSendPacket;
    NetworkConvertData ConvertData;
    #endregion
    private void Awake()
    {
        if (NWManager != null)
            GameObject.Destroy(gameObject);
        else
            NWManager = this;
        DontDestroyOnLoad(this);
        receiveData = "";
        sendData = "";
        ConvertData = new NetworkConvertData();
        networkRoom = new NetworkRoom();
        NetworkConnect();
    }
    void Start()
    {
    }
    void Update()
    {

    }
    public void NetworkConnect()
    {
        serverIP = PlayerPrefs.GetString("IP", "127.0.0.1"); // 35.87.152.15
        port = PlayerPrefs.GetInt("Port", 1108); // 2222
        if (serverIP == "")
            Debug.LogError("Server IP Address not set");
        if (port == -1)
            Debug.LogError("Port not set");
        endPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
        dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.Blocking = false;
        id = CreateID();
        coroutineSendPacket = StartCoroutine(SendPacket());
        coroutineReceivePacket = StartCoroutine(ReceivePacket());
    }
    public void NetworkDisconnect()
    {
        if (udp.Connected)
        {
            StopCoroutine(coroutineSendPacket);
            StopCoroutine(coroutineReceivePacket);
            receiveData = "";
            canReceiveData = false;
            NetworkClientData data = new NetworkClientData(NetworkManager.NWManager.id, false);
            string json = ConvertData.ConvertNetworkClientDataToJsonString(data);
            byte[] arr = Encoding.ASCII.GetBytes(json);
            udp.SendTo(arr, endPoint);
            udp.Close();
            GameObject.Find("NetworkClient").GetComponent<NetworkClientDisplay>().ClearClient();
            // Debug.Log(json);
        }
    }
    public void ChangeIPAndPort(string ip, int port)
    {
        PlayerPrefs.SetString("IP", ip);
        PlayerPrefs.SetInt("Port", port);
        this.serverIP = ip;
        this.port = port;
    }
    public Socket UDP()
    {
        return udp;
    }
    public IPEndPoint IP_EndPoint()
    {
        return endPoint;
    }
    public EndPoint Dummy_EndPoint()
    {
        return dummyEndpoint;
    }
    public string CreateID()
    {
        string value = DateTime.Now.ToString();
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }
        return Sb.ToString();
    }
    IEnumerator SendPacket()
    {
        while (true)
        {
            yield return null;
            if (sendData.Equals(""))
            {
                continue;
            }
            else
            {
                byte[] arr = Encoding.ASCII.GetBytes(sendData);
                udp.SendTo(arr, endPoint);
                // Debug.Log(sendData);
            }
        }
    }
    IEnumerator ReceivePacket()
    {
        byte[] buffer = new byte[16384];
        canReceiveData = true;
        while (true)
        {
            yield return null;
            if (udp.Available != 0)
            {
                Array.Clear(buffer, 0, buffer.Length);
                int receiveBuf = udp.ReceiveFrom(buffer, ref dummyEndpoint);
                receiveData = Encoding.Default.GetString(buffer);
                // Debug.Log(receiveData);
            }
        }
    }
    void OnApplicationQuit()
    {
        NetworkDisconnect();
    }
}
