using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
namespace BKSpeed
{
    public class ClientController : MonoBehaviour
    {
        [SerializeField]
        float m_speed = 10f;
        #region "Public Members"
        #endregion

        #region "Private Members"
        float m_horizontal, m_vertical;
        Rigidbody m_rigidbody;
        NetworkClient client;
        bool isMaster = false; // true: có thể điều kiển, false: không thể điều khiển
        #endregion

        void Start()
        {
            client = GameObject.Find("NetworkClient").GetComponent<NetworkClient>();
            m_rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (!isMaster) return;
            GetMoveInput();
            if (m_horizontal != 0 || m_vertical != 0)
            {
                bool j = false;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    j = true;
                }
                Move(m_horizontal, m_vertical, j);
            }
            client.SendPacket(new ClientTransform(transform));
        }

        void GetMoveInput()
        {
            m_horizontal = Input.GetAxis("Horizontal");
            m_vertical = Input.GetAxis("Vertical");
        }
        void Move(float horizontal, float vertical, bool jump)
        {
            m_rigidbody.velocity = new Vector3(horizontal * m_speed, m_rigidbody.velocity.y, vertical * m_speed);
            if (jump)
            {
                m_rigidbody.velocity += Vector3.up * 20f;
            }
        }
        public void SetMaster(bool value)
        {
            isMaster = value;
        }
    }
}