using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] public string playerName;
    [SerializeField] private int classNo;

    public List<Vector3> startPos = new List<Vector3>();

    private void Awake()
    {
        startPos.Add(new Vector3(-8.07f, 0.1f, -12.29f));
        startPos.Add(new Vector3(-5.4f, 0.1f, -6f));
        DestroyCheck();
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    private void Start()
    {

    }
    public void PlayerSetting(string s, int n)
    {
        playerName = s;
        classNo = n;
    }
    public void DestroyCheck()
    {
        var obj = FindObjectsOfType<NetworkManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Sucess");
    }
    public void JoinOrCreateRoom()
    {
        switch (classNo) 
        {
            case 1:
                PhotonNetwork.JoinOrCreateRoom("ClassRoom", new RoomOptions { MaxPlayers = 5 }, null);
                Debug.Log("Join classroom");
                break;
            case 2:
                PhotonNetwork.JoinOrCreateRoom("ServerRoom", new RoomOptions { MaxPlayers = 5 }, null);
                Debug.Log("Join ServerRoom");
                break;
        }

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerArmature", startPos[classNo-1], Quaternion.identity);
    }
}
