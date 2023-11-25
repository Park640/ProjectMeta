using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class AudioController : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private AudioSource audioS;
    [SerializeField] private PhotonView PV;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(audioS);
        }
        else
        {
            this.audioS = (AudioSource)stream.ReceiveNext();
        }
    }
}
