using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
namespace BKSpeed
{
    [RequireComponent(typeof(NetworkClientDisplay))]
    public class NetworkClient : MonoBehaviour
    {
        #region "Public Members"
        #endregion

        #region "Private Members"
        NetworkConvertData ConvertData;
        ClientTransform tempClientTransform;
        #endregion

        void Awake()
        {
            ConvertData = new NetworkConvertData();
            tempClientTransform = new ClientTransform();
        }
        private void Start()
        {

        }
        void Update()
        {

        }

        public void SendPacket(ClientTransform clientTransform)
        {
            tempClientTransform = clientTransform;
            UserDataRealtime data = new UserDataRealtime(NetworkManager.NWManager.id, true, clientTransform);
            NetworkManager.NWManager.userSendData = data;
            // string json = ConvertData.ConvertNetworkClientDataToJsonString(data);
        }

        void AddOtherClient(string parsedID, Transform trans)
        {

        }
    }
}