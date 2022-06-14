using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
namespace BKSpeed
{
    public class UIManager : MonoBehaviour
    {
        public TMP_InputField txt_ip;
        public TMP_InputField txt_port;
        public TMP_Text txt_btnConnect;
        public Button btn_ButtonConnect;
        public bool btnModify = true; // true: connect, false: disconnect
        private void Start()
        {
            txt_ip.text = PlayerPrefs.GetString("IP", "127.0.0.1");
            txt_port.text = PlayerPrefs.GetInt("Port", 1108).ToString();
            btnModify = !NetworkManager.NWManager.isConnected;
        }
        public void ClickButtonConnect()
        {
            if (btnModify)
            {
                btn_ButtonConnect.enabled = false;
                UDPConnect();
            }
            else
            {
                btn_ButtonConnect.enabled = false;
                txt_btnConnect.text = "Connect";
                UDPDisconnect();
            }
        }
        void UDPConnect()
        {
            StartCoroutine(ConnectServer());
        }
        void UDPDisconnect()
        {
            NetworkManager.NWManager.NetworkDisconnect();
            btnModify = true;
            btn_ButtonConnect.enabled = true;
        }
        IEnumerator ConnectServer()
        {
            PlayerPrefs.SetString("IP", txt_ip.text);
            PlayerPrefs.SetInt("Port", Int32.Parse(txt_port.text));
            for (int i = 0; i < 2; i++)
            {
                txt_btnConnect.text = (2 - i).ToString() + " ...";
                yield return new WaitForSeconds(1f);
            }
            NetworkManager.NWManager.NetworkConnect();
            txt_btnConnect.text = "Disconnect";
            btn_ButtonConnect.enabled = true;
            btnModify = false;
        }
    }
}
