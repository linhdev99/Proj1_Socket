using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkClientDisplay : MonoBehaviour
{
    #region "Public Members"
    public GameObject go_Ball;
    public Dictionary<string, NetworkClientData> clients;
    public Transform spawnUser;
    #endregion

    #region "Private Members"
    NetworkConvertData ConvertData;
    Dictionary<string, GameObject> clientsInScene;
    #endregion
    private void Awake()
    {
        ConvertData = new NetworkConvertData();
        clients = new Dictionary<string, NetworkClientData>();
        clientsInScene = new Dictionary<string, GameObject>();
    }
    private void Start()
    {
        CreateMasterUser();
    }
    private void Update()
    {
        if (NetworkManager.NWManager.canReceiveData)
        {
            GetData();
        }
    }
    private void CreateMasterUser()
    {
        if (NetworkManager.NWManager.id != "" || NetworkManager.NWManager.id != null)
        {
            GameObject ball = Instantiate(go_Ball, spawnUser.position, spawnUser.rotation);
            ball.GetComponent<ClientController>().SetMaster(true);
            ball.name = NetworkManager.NWManager.id;
            clientsInScene.Add(NetworkManager.NWManager.id, ball);
        }
    }
    private void GetData()
    {
        clients = ConvertData.ConvertJsonStringToNetworkClientData(NetworkManager.NWManager.receiveData);
        if (clients == null)
        {
            return;
        }
        foreach (string key in clients.Keys)
        {
            if (key.Equals(NetworkManager.NWManager.id))
            {
                continue;
            }
            else if (!clientsInScene.ContainsKey(key))
            {
                GameObject ball = Instantiate(go_Ball, clients[key].clientTransform.GetPosition(), clients[key].clientTransform.GetRotation());
                ball.transform.localScale = clients[key].clientTransform.GetScale();
                ball.name = key;
                ball.GetComponent<ClientController>().SetMaster(false);
                clientsInScene.Add(key, ball);
            }
            else
            {
                clientsInScene[key].transform.position = clients[key].clientTransform.GetPosition();
                clientsInScene[key].transform.rotation = clients[key].clientTransform.GetRotation();
                clientsInScene[key].transform.localScale = clients[key].clientTransform.GetScale();
            }
        }
    }
    public void ClearClient()
    {
        clients.Clear();
        if (clientsInScene.Count > 0)
        {
            foreach (string key in clientsInScene.Keys)
            {
                Destroy(clientsInScene[key].gameObject);
            }
            clientsInScene.Clear();
        }
    }
}
