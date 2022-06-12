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
        NetworkClientData data = new NetworkClientData(NetworkManager.NWManager.id, true, clientTransform);
        string json = ConvertData.ConvertNetworkClientDataToJsonString(data);
        NetworkManager.NWManager.sendData = json;
    }

    void AddOtherClient(string parsedID, Transform trans)
    {

    }
}
