using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClientDisplay : MonoBehaviour
{
    public GameObject go_Ball;
    public Dictionary<string, NetworkClientData> clients;
    NetworkConvertData ConvertData;
    Dictionary<string, GameObject> clientsInScene;
    private void Awake()
    {
        // client = new OtherClient("", false, Vector3.zero, Quaternion.identity, Vector3.zero);
        // ball_clone = Instantiate(go_Ball, go_Ball.transform.position, go_Ball.transform.rotation);
        ConvertData = new NetworkConvertData();
        clients = new Dictionary<string, NetworkClientData>();
        clientsInScene = new Dictionary<string, GameObject>();
    }
    void Update()
    {
        // transform.position = client.position;
        // ball_clone.transform.position = client.position;
        // ball_clone.transform.rotation = client.rotation;
        // ball_clone.transform.localScale = client.scale;
        // Debug.Log(client.scale);
        GetData();
    }
    public void SetData(TransformGO trans)
    {
        // client.clientID = trans.clientID;
        // client.state = trans.state;
        // client.lpos = trans.position;
        // client.lrot = trans.rotation;
        // client.lscale = trans.scale;
    }
    public void GetData()
    {
        clients = ConvertData.ConvertJsonStringToNetworkClientData(NetworkManager.NWManager.receiveData);
        if (clients == null)
        {
            return;
        }
        foreach (string key in clients.Keys)
        {
            if (key.Equals(NetworkManager.NWManager.id))
            {
                continue;
            }
            if (!clientsInScene.ContainsKey(key))
            {
                GameObject ball = Instantiate(go_Ball, clients[key].clientTransform.GetPosition(), clients[key].clientTransform.GetRotation());
                ball.transform.localScale = clients[key].clientTransform.GetScale();
                clientsInScene.Add(key, ball);
            }
            else
            {
                clientsInScene[key].transform.position = clients[key].clientTransform.GetPosition();
                clientsInScene[key].transform.rotation = clients[key].clientTransform.GetRotation();
                clientsInScene[key].transform.localScale = clients[key].clientTransform.GetScale();
            }
        }
    }
}
