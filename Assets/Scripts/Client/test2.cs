using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
namespace BKSpeed
{
    public class test2 : MonoBehaviour
    {
        // Start is called before the first frame update
        MulticastUdpClient udpClientWrapper;
        void Start()
        {
            // Create address objects
            int port = 7766;
            IPAddress multicastIPaddress = IPAddress.Parse("230.0.0.1");
            IPAddress localIPaddress = IPAddress.Any;

            // Create MulticastUdpClient
            udpClientWrapper = new MulticastUdpClient(multicastIPaddress, port, localIPaddress);
            // string receivedText = Encoding.ASCII.GetString(udpClientWrapper.);
            udpClientWrapper.UdpMessageReceived += OnUdpMessageReceived;

            Debug.Log("UDP Client started");
        }
        void OnUdpMessageReceived(object sender, MulticastUdpClient.UdpMessageReceivedEventArgs e)
        {
            string receivedText = ASCIIEncoding.Unicode.GetString(e.Buffer);
            Debug.Log("Received message: " + receivedText);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}