using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace OpenAI
{
    public class AboveUI : MonoBehaviour
    {
        private NetworkManager network;

        public TextMeshProUGUI nickName;
        public GameObject Player;

        private void Awake()
        {
            network = FindObjectOfType<NetworkManager>();
            nickName.text = network.playerName;
        }
      
    }
}


