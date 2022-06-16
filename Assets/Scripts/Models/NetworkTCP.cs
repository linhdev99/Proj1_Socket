using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace BKSpeed
{
    public class NetworkTCP
    {
        public Action<int> actionJoinRoomWithPort;
        private IPAddress ip;
        private int port;
        private Socket tcp;
        IPEndPoint endPoint;
        byte[] receiveData;
        string jsonData = "";
        private NetworkConvertData ConvertData;
        private Thread threadReceivePacket;
        public NetworkTCP(string ip, int port)
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
            receiveData = new byte[BaseConstant.MAX_BUFFER_SIZE];
            ConvertData = new NetworkConvertData();
            actionJoinRoomWithPort = NetworkManager.NWManager.JoinRoomWithPort;
            Connect();
        }

        private void Connect()
        {
            endPoint = new IPEndPoint(ip, port);
            tcp = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            tcp.SendTimeout = BaseConstant.SOCKET_TCP_SEND_TIMEOUT;
            try
            {
                tcp.Connect(endPoint);
                Debug.Log(tcp.RemoteEndPoint);
                threadReceivePacket = new Thread(ReceivePacket);
                threadReceivePacket.Start();
            }
            catch (System.Exception e)
            {
                Debug.LogErrorFormat("TCP Connect: {0}", e);
            }
        }
        public void Disconnect()
        {
            if (tcp != null)
            {
                tcp.Close();
            }
            if (threadReceivePacket != null)
            {
                threadReceivePacket.Abort();
            }
        }
        void ReceivePacket()
        {
            MessageSocket messageSocket = new MessageSocket();
            while (true)
            {
                if (tcp.Connected)
                {
                    tcp.Receive(receiveData);
                    jsonData = Encoding.ASCII.GetString(receiveData, 0, receiveData.Length);
                    messageSocket = ConvertData.Convert_JsonString_To_MessageSocket(jsonData);
                    handleMessageSocket(messageSocket);
                    Array.Clear(receiveData, 0, receiveData.Length);
                }
            }
        }
        public void SocketTCPSendPacket(MessageSocket messageSocket)
        {
            byte[] sendData = new byte[1024];
            string strSendDataFormat = string.Format("{0}<EOF>", ConvertData.Convert_MessageSocket_To_JsonString(messageSocket));
            sendData = Encoding.ASCII.GetBytes(strSendDataFormat);
            int bytesSend = tcp.Send(sendData);
            Array.Clear(sendData, 0, sendData.Length);
        }

        private void handleMessageSocket(MessageSocket messageSocket)
        {
            if (messageSocket == null) return;
            string action = messageSocket.getAction();
            switch (action)
            {
                case BaseConstant.SOCKET_EVENT_CREATE_ROOM_SUCCESS:
                    {
                        Dictionary<string, int> udpPorts = new Dictionary<string, int>();
                        udpPorts = ConvertData.Convert_JsonString_To_DictInt(messageSocket.getValue());
                        int portData = udpPorts["portData"];
                        int portRoom = udpPorts["portRoom"];
                        actionJoinRoomWithPort(portData);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}