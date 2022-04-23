using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody m_rig;
    float speed;
    void Start()
    {
        m_rig = GetComponent<Rigidbody>();
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool j = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            j = true;
        }
        Move(h, v, j);
        SetDataToServer();
    }

    private void SetDataToServer()
    {
        Vector3 temp_pos = transform.position;
        Quaternion temp_rot = transform.rotation;
        Vector3 temp_scale = transform.localScale;
        GameManager.GM.SetDataServer(temp_pos, temp_rot, temp_scale);
        //------------------- Position
        // string pos_x = temp_pos.x.ToString();
        // string pos_y = temp_pos.y.ToString();
        // string pos_z = temp_pos.z.ToString();
        //------------------- Rotation
        // string rot_x = temp_rot.x.ToString();
        // string rot_y = temp_rot.y.ToString();
        // string rot_z = temp_rot.z.ToString();
        // string rot_w = temp_rot.z.ToString();
        //------------------- Scale
        // string scale_x = temp_scale.x.ToString();
        // string scale_y = temp_scale.y.ToString();
        // string scale_z = temp_scale.z.ToString();

        // Debug.LogFormat("Position: {0},{1},{2}\n" +
        //                 "Rotation: {3},{4},{5},{6}\n" +
        //                 "Scale: {7},{8},{9}",
        //                 pos_x, pos_y, pos_z, rot_x, rot_y, rot_z, rot_w, scale_x, scale_y, scale_z);
    }

    void Move(float horizontal, float vertical, bool jump)
    {
        m_rig.velocity = new Vector3(horizontal * speed, m_rig.velocity.y, vertical * speed);
        if (jump)
        {
            m_rig.velocity += Vector3.up * 20f;
        }
        else
        {

        }
    }
}
