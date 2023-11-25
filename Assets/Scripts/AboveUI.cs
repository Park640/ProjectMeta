using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace OpenAI
{
    public class AboveUI : MonoBehaviourPun, IPunObservable
    {
        private NetworkManager network;

        public TextMeshProUGUI nickName;
        public GameObject Player;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(nickName.text);
            }
            else
            {
                this.nickName.text = (string)stream.ReceiveNext();
            }
        }
        private void Awake()
        {
            network = FindObjectOfType<NetworkManager>();
            nickName.text = network.playerName;
        }
      
    }
}


