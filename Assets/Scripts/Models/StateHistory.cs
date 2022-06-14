using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
