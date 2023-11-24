using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;


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


    private PlayerInfo pInfo = new PlayerInfo();

    private int sceneNo = 1;
    private bool start = true;

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
        sceneNo = n;
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
        pInfo.Pname = nameSpace.text;
        pInfo.classRoom = sceneNo;
        tcpClient.PacketTransfer(DefaultPacket.Serialize(pInfo));
        SceneManager.LoadScene(sceneNo);
    }

    
}



