using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class BallClone : MonoBehaviour
{
    Rigidbody m_rig;
    OptTransform m_optTrans;

    void Start()
    {
        m_optTrans = null;
        m_rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetDataFromServer();
        SetTransformBall();
    }
    void GetDataFromServer()
    {
        string jsonData = "";
        jsonData = GameManager.GM.GetDataServer();
        if (jsonData == null || jsonData.Equals(""))
        {
            return;
        }
        m_optTrans = JsonConvert.DeserializeObject<OptTransform>(jsonData);
        // Debug.LogFormat("Position: {0},{1},{2}\n" +
        //                 "Rotation: {3},{4},{5},{6}\n" +
        //                 "Scale: {7},{8},{9}",
        //                 m_optTrans.position[0], m_optTrans.position[1], m_optTrans.position[2],
        //                 m_optTrans.rotation[0], m_optTrans.rotation[1], m_optTrans.rotation[2], m_optTrans.rotation[3],
        //                 m_optTrans.scale[0], m_optTrans.scale[1], m_optTrans.scale[2]);
    }
    void SetTransformBall()
    {
        if (m_optTrans == null) return;
        transform.position = new Vector3(m_optTrans.position[0] - 15, m_optTrans.position[1], m_optTrans.position[2]);
        transform.localScale = new Vector3(m_optTrans.scale[0], m_optTrans.scale[1], m_optTrans.scale[2]);
        transform.rotation = new Quaternion(m_optTrans.rotation[0], m_optTrans.rotation[1], m_optTrans.rotation[2], m_optTrans.rotation[3]);
    }
}
