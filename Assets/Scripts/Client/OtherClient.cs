using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherClient
{
    public string clientID;
    public bool state; // true: connect, false: disconnect
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public List<float> lpos
    {
        set
        {
            if (value.Count < 3) return;
            position = new Vector3(value[0] - 15, value[1], value[2]);
        }
    }
    public List<float> lrot
    {
        set
        {
            if (value.Count < 4) return;
            rotation = new Quaternion(value[0], value[1], value[2], value[3]);
        }
    }
    public List<float> lscale
    {
        set
        {
            if (value.Count < 3) return;
            scale = new Vector3(value[0], value[1], value[2]);
        }
    }
    public OtherClient(string id, bool s, List<float> pos, List<float> rot, List<float> scal)
    {
        clientID = id;
        state = s;
        position = new Vector3(pos[0] - 15, pos[1], pos[2]);
        rotation = new Quaternion(rot[0], rot[1], rot[2], rot[3]);
        scale = new Vector3(scal[0], scal[1], scal[2]);
    }
    public OtherClient(string id, bool s, Vector3 pos, Quaternion rot, Vector3 scal)
    {
        clientID = id;
        state = s;
        position = pos;
        rotation = rot;
        scale = scal;
    }
}
