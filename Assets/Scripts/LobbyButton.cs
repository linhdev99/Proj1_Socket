using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    private Lobby m_lobby;
    private void Start()
    {
        m_lobby = GetComponent<Lobby>();
    }
    public void ClickButtonJoin()
    {
        m_lobby.JoinRoom();
    }
    public void ClickButtonNew()
    {
        m_lobby.NewRoom();
    }
    public void ClickButtonConnect()
    {
        m_lobby.UserConnect();
    }
}
