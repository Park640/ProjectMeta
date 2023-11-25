using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class BoardControl : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private PhotonView PV;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(text);
        }
        else
        {
            this.text = (TextMeshPro)stream.ReceiveNext();
        }
    }
}
