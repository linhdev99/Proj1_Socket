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
    // public string id;
    public int packetNumber;
    public Dictionary<int, StateHistory> history;
    // public Vector3 desiredPosition;
    [HideInInspector]
    public Transform nextTransform;
    #endregion

    #region "Private Members"
    Dictionary<string, OtherClient> otherClients;
    NetworkClientDisplay otherClientMover;
    Socket udp;
    // IPEndPoint endPoint;
    EndPoint dummyEndpoint;
    TransformGO receiveData;
    NetworkConvertData ConvertData;
    #endregion

    void Awake()
    {
        packetNumber = 0;
        nextTransform = transform;
        otherClientMover = GetComponent<NetworkClientDisplay>();
        otherClients = new Dictionary<string, OtherClient>();
        history = new Dictionary<int, StateHistory>();
        ConvertData = new NetworkConvertData();
        SendInitialReqToServer();
        history.Add(0, new StateHistory(transform));
    }
    private void Start()
    {
    }

    void SendInitialReqToServer()
    {
        udp = NetworkManager.NWManager.UDP();
        string p = ConvertData.ConvertTransformGOToJsonString(new TransformGO(transform, NetworkManager.NWManager.id, true));
        Debug.Log(p);
        byte[] packet = Encoding.ASCII.GetBytes(p);
        udp.SendTo(packet, NetworkManager.NWManager.IP_EndPoint());
    }

    public void SendPacket(TransformGO data)
    {
        if (NetworkManager.NWManager.id == null || NetworkManager.NWManager.id == "")
        {
            Debug.LogError("NOT Connected to server! (hint: start the server and then play the scene again");
            SendInitialReqToServer();
            return;
        }
        UpdateStateHistory();
        // byte[] arr = Encoding.ASCII.GetBytes(packetNumber + " " + id + " " + str);
        byte[] arr = Encoding.ASCII.GetBytes(ConvertData.ConvertTransformGOToJsonString(data));
        udp.SendTo(arr, NetworkManager.NWManager.IP_EndPoint());
    }

    public void UpdateStateHistory()
    {
        /* using desiredPosition because transform.position is used for lerping to desired position */
        history.Add(++packetNumber, new StateHistory(nextTransform));
        bool suc = history.Remove(packetNumber - 51);
    }

    void OnApplicationQuit()
    {
        TransformGO transGO = new TransformGO(transform, NetworkManager.NWManager.id, false);
        byte[] arr = Encoding.ASCII.GetBytes(ConvertData.ConvertTransformGOToJsonString(transGO));
        udp.SendTo(arr, NetworkManager.NWManager.IP_EndPoint());
        udp.Close();
    }

    void Update()
    {
        // if (udp.Available != 0)
        // {
        //     byte[] buffer = new byte[1024];
        //     // udp.Receive(buffer);
        //     dummyEndpoint = NetworkManager.NWManager.Dummy_EndPoint();
        //     int receiveBuf = udp.ReceiveFrom(buffer, ref dummyEndpoint);

        //     string data = Encoding.Default.GetString(buffer);
        //     receiveData = ConvertData.ConvertJsonStringToTransformGO(data);
        //     // otherClientMover.SetData(receiveData);
        //     // ballClone.SetTransformBall(receiveData);
        //     if (!receiveData.state)
        //     {
        //         Debug.Log(receiveData.clientID);
        //         return;
        //     }
        //     receiveData = null;
        //     // GC.Collect();
        //     // GC.WaitForPendingFinalizers();
        // }
    }

    void AddOtherClient(string parsedID, Transform trans)
    {
        // OtherClient other = new OtherClient(parsedID, true, trans.position, trans.rotation, trans.localScale);
        // otherClients.Add(parsedID, other);
    }
}
