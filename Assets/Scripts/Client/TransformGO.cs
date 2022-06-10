using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }
    public Vector3 GetScale()
    {
        return new Vector3(scale[0], scale[1], scale[2]);
    }
    public Quaternion GetRotation()
    {
        return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }
}