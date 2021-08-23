using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;

public class Connect : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Server'a girildi");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby girildi");
        PhotonNetwork.JoinOrCreateRoom("oda", new RoomOptions {MaxPlayers = 2, IsOpen = true, IsVisible = true},
            TypedLobby.Default);

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room girildi");

        GameObject test = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0 ,null);
        test.GetComponent<PhotonView>().Owner.NickName = "tallatalla";

    }

    public override void OnLeftRoom()
    {
        Debug.Log("Room cikildi");

    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobby cikildi");

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Hata: Odaya girilmedi");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("HJata: Herahngi bir odaya girilmedi");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Hata: Oda olusuturlamadi");
    }
}
