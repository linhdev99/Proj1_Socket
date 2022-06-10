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
    #endregion

    void Awake()
    {
        ConvertData = new NetworkConvertData();
        SendInitialReqToServer();
    }
    private void Start()
    {

    }
    void Update()
    {

    }

    void SendInitialReqToServer()
    {
        NetworkClientData data = new NetworkClientData(NetworkManager.NWManager.id, true);
        string json = ConvertData.ConvertNetworkClientDataToJsonString(data);
        NetworkManager.NWManager.sendData = json;
    }

    public void SendPacket(ClientTransform clientTransform)
    {
        if (NetworkManager.NWManager.id == null || NetworkManager.NWManager.id == "")
        {
            Debug.LogError("NOT Connected to server! (hint: start the server and then play the scene again");
            SendInitialReqToServer();
            return;
        }
        NetworkClientData data = new NetworkClientData(NetworkManager.NWManager.id, true, clientTransform);
        string json = ConvertData.ConvertNetworkClientDataToJsonString(data);
        // Debug.Log(json);
        NetworkManager.NWManager.sendData = json;
    }

    void OnApplicationQuit()
    {
        // TransformGO transGO = new TransformGO(transform, NetworkManager.NWManager.id, false);
        // byte[] arr = Encoding.ASCII.GetBytes(ConvertData.ConvertTransformGOToJsonString(transGO));
        // udp.SendTo(arr, NetworkManager.NWManager.IP_EndPoint());
        // udp.Close();
    }


    void AddOtherClient(string parsedID, Transform trans)
    {
        // OtherClient other = new OtherClient(parsedID, true, trans.position, trans.rotation, trans.localScale);
        // otherClients.Add(parsedID, other);
    }
}
