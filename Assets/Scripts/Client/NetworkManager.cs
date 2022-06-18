using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace BKSpeed
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager NWManager;
        #region "Public Members"
        #endregion

        #region "Private Members"
        NetworkConvertData ConvertData;
        private NetworkTCP networkTCP;
        private NetworkUDP networkUDP;
        private string id;

        #endregion
        private void Awake()
        {
            if (NWManager != null)
                GameObject.Destroy(gameObject);
            else
                NWManager = this;
            DontDestroyOnLoad(this);
            ConvertData = new NetworkConvertData();
            id = CreateId();
            // networkTCP = new NetworkTCP(BaseConstant.IP_ADDRESS, BaseConstant.PORT_SERVER);
            // CreateRoom(123456, id);
        }
        void Start()
        {
            JoinRoomWithPort(7766);
        }
        void Update()
        {

        }
        public void JoinRoomWithPort(int port)
        {
            // Debug.LogFormat("JOIN_ROOM {0}:{1}", BaseConstant.IP_ADDRESS, port.ToString());
            // networkUDP = new NetworkUDP(BaseConstant.IP_ADDRESS, port);
            // networkUDP.JoinGroup(port);
            // networkUDP.SendPacket(new UserDataRealtime(this.id, true, new ClientTransform(new List<float>() { 1, 1, 1 }, new List<float>() { 1, 1, 1, 2 }, new List<float>() { 3, 3, 3 })));
            UdpClient udp1 = new UdpClient();
            string groupEpAddress = "230.0.0.0";
            int groupEpPort = port;

            IPEndPoint groupEp = new IPEndPoint(IPAddress.Parse(groupEpAddress), groupEpPort);
            // udp1.Open(groupEp.Port);
            // udp1.
            udp1.JoinMulticastGroup(groupEp.Address);

            byte[] buffer = new byte[BaseConstant.MAX_BUFFER_SIZE];
            Debug.Log(udp1.Receive(ref groupEp));
            string receiveJsonData = Encoding.Default.GetString(buffer);
            Debug.Log(receiveJsonData);

        }
        public void testGetData(string data)
        {
            Debug.Log(data);
        }
        public string GetId()
        {
            return this.id;
        }
        public string CreateId()
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
        public void CreateRoom(int password, string idMaster)
        {
            Room room = new Room(password, idMaster);
            MessageSocket messageSocket = new MessageSocket(BaseConstant.SOCKET_EVENT_CREATE_ROOM, ConvertData.Convert_Room_To_JsonString(room));
            networkTCP.SocketTCPSendPacket(messageSocket);
        }
        private void OnApplicationQuit()
        {
            // networkTCP.Disconnect();
            // networkUDP.Disconnect();
        }

    }
}