using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace BKSpeed
{
    public class NetworkUDP
    {
        public Action<string> actionTest;
        private IPAddress ip;
        private int port;
        private Socket udp;
        private IPEndPoint endPoint;
        private EndPoint dummyEndpoint;
        private Thread threadReceive;
        private NetworkConvertData ConvertData;
        private string receiveJsonData;
        private string sendJsonData;
        private Dictionary<string, UserDataRealtime> receiveUserDataRealtime;

        public NetworkUDP(string ip, int port)
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
            ConvertData = new NetworkConvertData();
            actionTest = NetworkManager.NWManager.testGetData;
            Connect();
        }

        private void Connect()
        {
            endPoint = new IPEndPoint(ip, port);
            dummyEndpoint = new IPEndPoint(ip, port);
            udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udp.SendTimeout = BaseConstant.SOCKET_UDP_SEND_TIMEOUT;
            udp.ReceiveTimeout = BaseConstant.SOCKET_UDP_RECEIVE_TIMEOUT;
            udp.Blocking = false;
            receiveUserDataRealtime = new Dictionary<string, UserDataRealtime>();
            threadReceive = new Thread(ReceivePacket);
            threadReceive.Start();
        }
        public void Disconnect()
        {
            if (udp != null)
            {
                udp.Close();
            }
            if (threadReceive != null)
            {
                threadReceive.Abort();
            }
        }

        void ReceivePacket()
        {
            byte[] buffer = new byte[BaseConstant.MAX_BUFFER_SIZE];
            int receiveBuf = -1;
            while (true)
            {
                if (udp.Available != 0)
                {
                    receiveBuf = udp.ReceiveFrom(buffer, ref dummyEndpoint);
                    receiveJsonData = Encoding.Default.GetString(buffer);
                    receiveUserDataRealtime = ConvertData.Convert_JsonString_To_UserDataRealtime(receiveJsonData);
                    actionTest(receiveJsonData);
                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
        }

        public void SendPacket(UserDataRealtime data)
        {
            byte[] buffer = new byte[BaseConstant.MAX_BUFFER_SIZE];
            if (data != null)
            {
                sendJsonData = ConvertData.Convert_UserDataRealtime_To_JsonString(data);
                buffer = Encoding.ASCII.GetBytes(sendJsonData);
                udp.SendTo(buffer, endPoint);
                Array.Clear(buffer, 0, buffer.Length);
            }
        }

        public Dictionary<string, UserDataRealtime> getData()
        {
            return this.receiveUserDataRealtime;
        }
    }
}