using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using System.Net;
using System.Net.Sockets;

public class IntroUI : MonoBehaviour
{
    private const int MIN_NAME_LENGTH = 12;

    [SerializeField] private Button connectBtn;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TCPClient tcpClient;

    [SerializeField] GameObject intro;
    [SerializeField] GameObject playerInfo;

    [SerializeField] private Image classFrame;

    [SerializeField] RectTransform[] classBtn;

    [SerializeField] TMP_InputField nameSpace;

    [SerializeField] NetworkManager network;

    private PlayerInfo pInfo = new PlayerInfo();

    private int classNo = 1;
    private bool start = true;

    int no = 0;
    private void ButtonEnable()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            connectBtn.interactable = false;
        }
        else
        {
            connectBtn.interactable = true;
        }
    }

    public void ClassChoice(int n)
    {
        classFrame.rectTransform.position = classBtn[n - 1].position;
        classNo = n;
    }

    private void Update()
    {
        if (start && Input.anyKeyDown)
        {
            start = false;
            intro.SetActive(false);
            playerInfo.SetActive(true);
        }
        ButtonEnable();
    }

    public void SendPlayerInfo()
    {
        network.PlayerSetting(nameInput.text, classNo);
        network.JoinOrCreateRoom();
        SceneManager.LoadScene(classNo);
    }

    
}



