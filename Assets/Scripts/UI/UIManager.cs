using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public TMP_InputField txt_ip;
    public TMP_InputField txt_port;
    private void Start()
    {
        txt_ip.text = PlayerPrefs.GetString("IP", "127.0.0.1");
        txt_port.text = PlayerPrefs.GetInt("Port", 1108).ToString();
    }
    public void ClickButtonConnect()
    {
        PlayerPrefs.SetString("IP", txt_ip.text);
        PlayerPrefs.SetInt("Port", Int32.Parse(txt_port.text));
        SceneManager.LoadScene("Test1");
    }
}
