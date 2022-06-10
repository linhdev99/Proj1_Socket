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

public class StateHistory
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public StateHistory(Transform trans)
    {
        position = trans.position;
        rotation = trans.rotation;
        scale = trans.localScale;
    }
}
public class TransformGO
{
    public string clientID;
    public bool state; // true: connect, false: disconnect
    public List<float> position { get; set; }
    public List<float> rotation { get; set; }
    public List<float> scale { get; set; }
    public TransformGO(Transform trans = null, string id = "", bool s = true)
    {
        if (trans == null) return;
        clientID = id;
        state = s;
        position = new List<float> { Mathf.Round(trans.position.x * 10000) / 10000, Mathf.Round(trans.position.y * 10000) / 10000, Mathf.Round(trans.position.z * 10000) / 10000 };
        rotation = new List<float> { Mathf.Round(trans.rotation.x * 10000) / 10000, Mathf.Round(trans.rotation.y * 10000) / 10000, Mathf.Round(trans.rotation.z * 10000) / 10000, Mathf.Round(trans.rotation.w * 10000) / 10000 };
        scale = new List<float> { Mathf.Round(trans.localScale.x * 10000) / 10000, Mathf.Round(trans.localScale.y * 10000) / 10000, Mathf.Round(trans.localScale.z * 10000) / 10000 };
    }
}

[RequireComponent(typeof(NetworkClientDisplay))]
public class NetworkClient : MonoBehaviour
{
    // set it to your server address
    [SerializeField] string serverIP = "127.0.0.1";
    [SerializeField] int port = 8080;

    #region "Public Members"
    public string id;
    public int packetNumber;
    public Dictionary<int, StateHistory> history;
    // public Vector3 desiredPosition;
    [HideInInspector]
    public Transform nextTransform;
    public BallClone ballClone;
    #endregion

    #region "Private Members"
    Dictionary<string, GameObject> otherClients;
    NetworkClientDisplay otherClientMover;
    Socket udp;
    IPEndPoint endPoint;
    EndPoint dummyEndpoint;
    TransformGO receiveData;
    #endregion

    void Awake()
    {
        if (serverIP == "")
            Debug.LogError("Server IP Address not set");
        if (port == -1)
            Debug.LogError("Port not set");

        packetNumber = 0;
        // desiredPosition = transform.position;
        nextTransform = transform;
        otherClientMover = GetComponent<NetworkClientDisplay>();
        otherClients = new Dictionary<string, GameObject>();
        history = new Dictionary<int, StateHistory>();
        endPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.Blocking = false;
        id = GetID();
        // n stands for new user
        // server will reply with a unique id for this user
        SendInitialReqToServer();
        history.Add(0, new StateHistory(transform));
    }

    void SendInitialReqToServer()
    {
        // string p = "n " + transform.position.x + " " + transform.position.y + " " + transform.position.z;
        string p = ConvertTransformGOToJsonString(new TransformGO(transform, id, true));
        byte[] packet = Encoding.ASCII.GetBytes(p);
        udp.SendTo(packet, endPoint);
    }

    public void SendPacket(TransformGO data)
    {
        // TransformGO data = ConvertJsonStringToTransformGO(str);
        if (id == null || id == "")
        {
            Debug.LogError("NOT Connected to server! (hint: start the server and then play the scene again");
            SendInitialReqToServer();
            return;
        }
        UpdateStateHistory();
        // byte[] arr = Encoding.ASCII.GetBytes(packetNumber + " " + id + " " + str);
        byte[] arr = Encoding.ASCII.GetBytes(ConvertTransformGOToJsonString(data));
        udp.SendTo(arr, endPoint);
    }

    public void UpdateStateHistory()
    {
        /* using desiredPosition because transform.position is used for lerping to desired position */
        history.Add(++packetNumber, new StateHistory(nextTransform));
        bool suc = history.Remove(packetNumber - 51);
    }

    void OnApplicationQuit()
    {
        TransformGO transGO = new TransformGO(transform, id, false);
        byte[] arr = Encoding.ASCII.GetBytes(ConvertTransformGOToJsonString(transGO));
        udp.SendTo(arr, endPoint);
        udp.Close();
    }

    void Update()
    {
        if (udp.Available != 0)
        {
            byte[] buffer = new byte[1024];
            // udp.Receive(buffer);
            dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
            int receiveBuf = udp.ReceiveFrom(buffer, ref dummyEndpoint);

            string data = Encoding.Default.GetString(buffer);
            receiveData = ConvertJsonStringToTransformGO(data);
            ballClone.SetTransformBall(receiveData);
            if (!receiveData.state)
            {
                Debug.Log(receiveData.clientID);
                return;
            }
            receiveData = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    void AddOtherClient(string parsedID, Vector3 pos)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = parsedID;
        go.transform.position = pos;
        otherClients.Add(parsedID, go);
    }
    string ConvertTransformGOToJsonString(TransformGO transGO)
    {
        string jsonString = JObject.FromObject(transGO).ToString();
        return jsonString;
    }
    TransformGO ConvertJsonStringToTransformGO(string rawString)
    {
        TransformGO transGO = null;
        JObject json = JObject.Parse(rawString);
        transGO = json.ToObject<TransformGO>();
        return transGO;
    }
    public string GetID()
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
