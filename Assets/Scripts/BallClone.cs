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
        m_optTrans = GameManager.GM.GetDataServer();
        if (m_optTrans != null)
        {
            SetTransformBall();
        }
        // string jsonData = "";
        // if (jsonData == null || jsonData.Equals(""))
        // {
        //     return;
        // }
        // // string test = @"{""position"":[5.917352,3.404661,-2.49365664],""rotation"":[0.0208070148,2.71946163E-07,-0.0040278337,0.9997754],""scale"":[5.0,5.0,5.0]}";
        // m_optTrans = JsonConvert.DeserializeObject<OptTransform>(jsonData); // string (json) --> class
    }
    void SetTransformBall()
    {
        if (m_optTrans == null) return;
        transform.position = new Vector3(m_optTrans.position[0] - 15, m_optTrans.position[1], m_optTrans.position[2]);
        transform.localScale = new Vector3(m_optTrans.scale[0], m_optTrans.scale[1], m_optTrans.scale[2]);
        transform.rotation = new Quaternion(m_optTrans.rotation[0], m_optTrans.rotation[1], m_optTrans.rotation[2], m_optTrans.rotation[3]);
    }
}
