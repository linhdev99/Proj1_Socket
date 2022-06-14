using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
namespace BKSpeed
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager NWManager;
        string serverIP = "127.0.0.1";
        int port = 8080;
        #region "Public Members"
        public string id;
        // public Dictionary<string, NetworkClientData> networkReceiveData;
        // public NetworkClientData networkSendData;
        public Dictionary<string, UserDataRealtime> userReceiveData;
        public UserDataRealtime userSendData;
        public bool isConnected;
        #endregion

        #region "Private Members"
        Socket udp;
        Socket tcp;
        IPEndPoint endPoint;
        EndPoint dummyEndpoint;
        Coroutine coroutineReceivePacket;
        Coroutine coroutineSendPacket;
        NetworkConvertData ConvertData;
        private string receiveData;
        private string sendData;
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
            isConnected = false;
            // TCPConnect();
        }
        void Start()
        {
            // NetworkConnect();
            TCPConnect();
        }
        void Update()
        {

        }
        public void NetworkConnect()
        {
            serverIP = BaseConstant.IP_ADDRESS;
            port = BaseConstant.PORT_SERVER;
            if (serverIP == "")
                Debug.LogError("Server IP Address not set");
            if (port == -1)
                Debug.LogError("Port not set");
            endPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
            dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
            udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udp.Blocking = false;
            // create defaut user data
            id = CreateID();
            userSendData = new UserDataRealtime(id, true);
            userReceiveData = new Dictionary<string, UserDataRealtime>();
            coroutineSendPacket = StartCoroutine(SendPacket());
            coroutineReceivePacket = StartCoroutine(ReceivePacket());
            isConnected = true;
            GameObject.Find("NetworkClient").GetComponent<NetworkClientDisplay>().StartGame();
        }
        public void TCPConnect()
        {
            serverIP = BaseConstant.IP_ADDRESS;
            port = 7777;
            IPHostEntry host = Dns.GetHostEntry(serverIP);
            IPAddress iPAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(iPAddress, port);

            tcp = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            tcp.SendTimeout = 5000;
            // tcp.ReceiveTimeout = 5000;
            try
            {
                tcp.Connect(remoteEP);
                Debug.LogFormat("{0}", tcp.RemoteEndPoint.ToString());
                Thread a = new Thread(ReceiveTCP);
                a.Start();
                SendTCP();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }
        void ReceiveTCP()
        {
            byte[] receiveDataTcp = new byte[1024];
            while (true)
            {
                if (tcp.Connected)
                {
                    tcp.Receive(receiveDataTcp);
                    string text = Encoding.ASCII.GetString(receiveDataTcp, 0, receiveDataTcp.Length);
                    Debug.Log("receive  " + text);
                    MessageSocket abc = ConvertData.Convert_JsonString_To_MessageSocket(text);
                    Array.Clear(receiveDataTcp, 0, receiveDataTcp.Length);
                }
            }
        }
        void SendTCP()
        {
            byte[] sendData = new byte[1024];
            Room room = new Room(123, 123456, "asdad");
            MessageSocket testTxt = new MessageSocket(BaseConstant.SOCKET_EVENT_CREATE_ROOM, ConvertData.Convert_Room_To_JsonString(room));
            Debug.Log(ConvertData.Convert_MessageSocket_To_JsonString(testTxt));
            sendData = Encoding.ASCII.GetBytes(ConvertData.Convert_MessageSocket_To_JsonString(testTxt) + "<EOF>");
            // Debug.Log(sendData);
            // sendData = Encoding.ASCII.GetBytes("CREATE_ROOM|123456"+"<EOF>");
            int bytesSend = tcp.Send(sendData);
            Debug.Log(bytesSend);
            Array.Clear(sendData, 0, sendData.Length);
        }
        public void NetworkDisconnect()
        {
            if (udp.Connected)
            {
                // clear user on scene
                GameObject.Find("NetworkClient").GetComponent<NetworkClientDisplay>().ClearClient();
                // stop send and receive packet
                StopCoroutine(coroutineSendPacket);
                StopCoroutine(coroutineReceivePacket);
                // clear data
                userReceiveData.Clear();
                userReceiveData = null;
                userSendData = null;
                // send the last packet
                UserDataRealtime data = new UserDataRealtime(NetworkManager.NWManager.id, false);
                string json = ConvertData.Convert_UserDataRealtime_To_JsonString(data);
                byte[] arr = Encoding.ASCII.GetBytes(json);
                udp.SendTo(arr, endPoint);
                // close UDP
                udp.Close();
                // set state false
                isConnected = false;
                Debug.Log("Send false");
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
                if (userSendData != null)
                {
                    sendData = ConvertData.Convert_UserDataRealtime_To_JsonString(userSendData);
                    byte[] arr = Encoding.ASCII.GetBytes(sendData);
                    udp.SendTo(arr, endPoint);
                }
            }
        }
        IEnumerator ReceivePacket()
        {
            byte[] buffer = new byte[16384];
            while (true)
            {
                yield return null;
                if (udp.Available != 0)
                {
                    Array.Clear(buffer, 0, buffer.Length);
                    int receiveBuf = udp.ReceiveFrom(buffer, ref dummyEndpoint);
                    receiveData = Encoding.Default.GetString(buffer);
                    userReceiveData = ConvertData.Convert_JsonString_To_UserDataRealtime(receiveData);
                }
            }
        }
        void OnApplicationQuit()
        {
            if (isConnected) NetworkDisconnect();
            if (tcp != null) if (tcp.Connected) tcp.Close();
        }
    }
}