using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager NWManager;
    string serverIP = "127.0.0.1";
    int port = 8080;
    #region "Public Members"
    public string id;
    #endregion

    #region "Private Members"
    Socket udp;
    IPEndPoint endPoint;
    EndPoint dummyEndpoint;
    #endregion
    private void Awake()
    {
        if (NWManager != null)
            GameObject.Destroy(gameObject);
        else
            NWManager = this;
        DontDestroyOnLoad(this);
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
        serverIP = PlayerPrefs.GetString("IP", "127.0.0.1");
        port = PlayerPrefs.GetInt("Port", 1108);
        if (serverIP == "")
            Debug.LogError("Server IP Address not set");
        if (port == -1)
            Debug.LogError("Port not set");
        endPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.Blocking = false;
        id = CreateID();
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
        return new IPEndPoint(IPAddress.Any, 0);
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
}
