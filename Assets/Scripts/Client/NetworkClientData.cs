using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClientData
{
    public string clientID { get; set; }
    public bool state { get; set; } // true: connect, false: disconnect
    public ClientTransform clientTransform;
    public NetworkClientData(string clientID, bool state, ClientTransform clientTransform = null)
    {
        this.clientID = clientID;
        this.state = state;
        this.clientTransform = clientTransform;
    }
}