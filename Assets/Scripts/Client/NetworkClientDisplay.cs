using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BKSpeed
{
    public class NetworkClientDisplay : MonoBehaviour
    {
        #region "Public Members"
        public GameObject go_Ball;
        public Dictionary<string, UserDataRealtime> clients;
        public Transform spawnUser;
        #endregion

        #region "Private Members"
        NetworkConvertData ConvertData;
        Dictionary<string, GameObject> clientsInScene;
        #endregion
        private void Awake()
        {
        }
        private void Start()
        {
            ConvertData = new NetworkConvertData();
            clients = new Dictionary<string, UserDataRealtime>();
            clientsInScene = new Dictionary<string, GameObject>();
        }
        private void Update()
        {
            // if (NetworkManager.NWManager.isConnectedToServer())
            // {
            //     GetData();
            // }
        }
        private void CreateMasterUser()
        {
            if (clientsInScene == null)
            {
                clientsInScene = new Dictionary<string, GameObject>();
            }
            // if (NetworkManager.NWManager.getId() != "" || NetworkManager.NWManager.getId() != null)
            // {
            //     GameObject ball = Instantiate(go_Ball, spawnUser.position, spawnUser.rotation);
            //     ball.GetComponent<ClientController>().SetMaster(true);
            //     ball.name = NetworkManager.NWManager.getId();
            //     clientsInScene.Add(NetworkManager.NWManager.getId(), ball);
            // }
        }
        private void GetData()
        {
            // clients = ConvertData.ConvertJsonStringToNetworkClientData(NetworkManager.NWManager.receiveData);
            // clients = NetworkManager.NWManager.getUDPReceiveUserDataRealtime();
            if (clients == null)
            {
                return;
            }
            foreach (string key in clients.Keys)
            {
                // if (key.Equals(NetworkManager.NWManager.getId()))
                // {
                //     continue;
                // }
                // else if (!clientsInScene.ContainsKey(key))
                // {
                //     GameObject ball = Instantiate(
                //                                     go_Ball,
                //                                     clients[key].getClientTransform().GetPosition(),
                //                                     clients[key].getClientTransform().GetRotation()
                //                                 );
                //     ball.transform.localScale = clients[key].getClientTransform().GetScale();
                //     ball.name = key;
                //     ball.GetComponent<ClientController>().SetMaster(false);
                //     clientsInScene.Add(key, ball);
                // }
                // else
                // {
                //     clientsInScene[key].transform.position = clients[key].getClientTransform().GetPosition();
                //     clientsInScene[key].transform.rotation = clients[key].getClientTransform().GetRotation();
                //     clientsInScene[key].transform.localScale = clients[key].getClientTransform().GetScale();
                // }
            }
        }
        public void ClearClient()
        {
            if (clients != null) clients.Clear();
            if (clientsInScene != null)
            {
                foreach (string key in clientsInScene.Keys)
                {
                    Destroy(clientsInScene[key].gameObject);
                }
                clientsInScene.Clear();
            }
        }
        public void StartGame()
        {
            // if (NetworkManager.NWManager.isConnectedToServer())
            // {
            //     CreateMasterUser();
            // }
        }
    }
}