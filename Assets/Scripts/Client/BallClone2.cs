using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class BallClone2 : MonoBehaviour
{
    // Start is called before the first frame update
    EndPoint dummyEndpoint;
    Socket udp;
    NetworkConvertData ConvertData;
    TransformGO receiveData;
    public OtherClient client;
    void Start()
    {
        udp = NetworkManager.NWManager.UDP();
        ConvertData = new NetworkConvertData();
        client = new OtherClient("", false, Vector3.zero, Quaternion.identity, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        ReceiveData();
        transform.position = client.position;
        transform.rotation = client.rotation;
        transform.localScale = client.scale;
    }
    void ReceiveData()
    {
        if (udp.Available != 0)
        {
            byte[] buffer = new byte[1024];
            dummyEndpoint = NetworkManager.NWManager.Dummy_EndPoint();
            int receiveBuf = udp.ReceiveFrom(buffer, ref dummyEndpoint);

            string data = Encoding.Default.GetString(buffer);
            receiveData = ConvertData.ConvertJsonStringToTransformGO(data);
            SetData(receiveData);
            if (!receiveData.state)
            {
                Debug.Log(receiveData.clientID);
                return;
            }
            receiveData = null;
            // GC.Collect();
            // GC.WaitForPendingFinalizers();
        }
    }

    public void SetData(TransformGO trans)
    {
        client.clientID = trans.clientID;
        client.state = trans.state;
        client.lpos = trans.position;
        client.lrot = trans.rotation;
        client.lscale = trans.scale;
    }
}
