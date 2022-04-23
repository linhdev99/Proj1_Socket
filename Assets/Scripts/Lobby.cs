using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using System;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    List<string> m_DropOptions;
    [SerializeField]
    TMP_Dropdown m_Dropdown;
    [SerializeField]
    TMP_InputField m_inputID;
    void Start()
    {
        // m_Dropdown.OnPointerClick('clicked');
        m_DropOptions = new List<string>();
    }
    void Update()
    {
    }
    public void ShowRoom()
    {
        // m_Dropdown.Hide();
        ReceiveRoomFromServer();
        int temp_value = m_Dropdown.value;
        m_Dropdown.ClearOptions();
        m_Dropdown.AddOptions(m_DropOptions);
        m_Dropdown.value = temp_value;
        m_Dropdown.Hide();
        m_Dropdown.Show();
    }
    public void JoinRoom()
    {
        try
        {
            Debug.Log(m_Dropdown.options[m_Dropdown.value].text);
            SceneManager.LoadScene("Test1");
        }
        catch (System.Exception)
        {
            Debug.Log("Choose Room!");
        }
    }
    private void ReceiveRoomFromServer()
    {
        // Get Room
        // m_DropOptions.Clear();
        // for (int i = 0; i < Random.Range(1, 10); i++)
        // {
        //     m_DropOptions.Add(i.ToString());
        // }
    }
    public void NewRoom()
    {
        if (m_DropOptions.Count > 19)
        {
            Debug.Log("So luong phong dat toi da, vui long cho!");
            return;
        }
        int newRoom = Random.Range(0, 20);
        while (CheckExistRoom(newRoom))
        {
            newRoom = Random.Range(0, 20);
        }
        SendRoomToServer(newRoom);
    }

    private bool CheckExistRoom(int newRoom)
    {
        if (m_DropOptions.Count == 0) return false;
        foreach (string room in m_DropOptions)
        {
            if (Int32.Parse(room) == newRoom)
            {
                return true; // ton tai
            }
        }
        return false; // chua ton tai
    }

    private void SendRoomToServer(int _room)
    {
        // Send
        m_DropOptions.Add(_room.ToString());
    }
    public void UserConnect()
    {
        Debug.Log(m_inputID.text);
    }
}
