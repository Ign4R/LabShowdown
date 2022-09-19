using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetManager : MonoBehaviourPunCallbacks
{
    public Button button;
    public TextMeshProUGUI status;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        button.interactable = false;
        status.text = "Connecting To Master";
    }
    public void Connected()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("Crash", options, TypedLobby.Default);
    }
    public override void OnConnectedToMaster()
    {
        button.interactable = false;
        PhotonNetwork.JoinLobby();
        status.text = "Connecting To Lobby";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        status.text = "Connection failed";
    }

    public override void OnJoinedLobby()
    {
        button.interactable = true;
        status.text = "Connected To Lobby";
    }
    public override void OnLeftLobby()
    {
        status.text = "Lobby failed";
    }
    public override void OnCreatedRoom()
    {
        status.text = "Created Room";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        status.text = "Created Room failed";
        button.interactable = false;
    }

    public override void OnJoinedRoom()
    {
        status.text = "Joined Room";
        PhotonNetwork.LoadLevel("Level");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        status.text = "Joined Room failed";
        button.interactable = false;
    }

}

