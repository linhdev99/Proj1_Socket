using System.Collections;
using System.Collections.Generic;
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
    }
    void GetDataFromServer()
    {
        m_optTrans = GameManager.GM.ReceiveDataFormServer();
        if (m_optTrans != null)
        {
            SetTransformBall();
        }
    }
    void SetTransformBall()
    {
        if (m_optTrans == null) return;
        transform.position = new Vector3(m_optTrans.position[0] - 15, m_optTrans.position[1], m_optTrans.position[2]);
        transform.localScale = new Vector3(m_optTrans.scale[0], m_optTrans.scale[1], m_optTrans.scale[2]);
        transform.rotation = new Quaternion(m_optTrans.rotation[0], m_optTrans.rotation[1], m_optTrans.rotation[2], m_optTrans.rotation[3]);
    }
}
