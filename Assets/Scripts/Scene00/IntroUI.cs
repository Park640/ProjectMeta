using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class IntroUI : MonoBehaviour
{
    private const int MIN_NAME_LENGTH = 12;

    [SerializeField] private Button connectBtn;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TCPClient tcpClient;
    private void ButtonEnable()
    {
        if (tcpClient.isConnceting == true)
        {
            connectBtn.interactable = false;
        }
        else
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
        
    }
    private void Update()
    {
        ButtonEnable();
    }
}
