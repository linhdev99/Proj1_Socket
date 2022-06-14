using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
namespace BKSpeed
{
    [RequireComponent(typeof(NetworkClient))]
    [RequireComponent(typeof(NetworkClientDisplay))]
    public class NetworkInputSync : MonoBehaviour
    {
        [Tooltip("The distance to be moved in each move input")]
        [SerializeField]
        float m_speed = 10f;
        float m_horizontal, m_vertical;
        Rigidbody m_rigidbody;
        NetworkClient client;
        NetworkClientDisplay clientMover;

        void Start()
        {
            client = GetComponent<NetworkClient>();
            clientMover = GetComponent<NetworkClientDisplay>();
            m_rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (NetworkManager.NWManager.id != "")
            {
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
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     client.SendPacket(new ClientTransform(transform));
            // }
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
    }
}