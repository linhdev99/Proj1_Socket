using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkClient))]
public class NetworkClientDisplay : MonoBehaviour
{
    public GameObject go_Ball;
    public OtherClient client;
    public GameObject ball_clone;
    private void Start()
    {
        client = new OtherClient("", false, Vector3.zero, Quaternion.identity, Vector3.zero);
        ball_clone = Instantiate(go_Ball, go_Ball.transform.position, go_Ball.transform.rotation);
    }
    void Update()
    {
        // transform.position = client.position;
        ball_clone.transform.position = client.position;
        ball_clone.transform.rotation = client.rotation;
        ball_clone.transform.localScale = client.scale;
        // Debug.Log(client.scale);
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
