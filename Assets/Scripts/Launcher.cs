using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomNameIP;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject UIbuttons;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject JoinLobby;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListPrefab;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private Transform playerListPrefab;
    [SerializeField] private Player[] players;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject nickNameMenu;

    void Start()
    {
        loading.SetActive(true);
        UIbuttons.SetActive(false);
        Debug.Log("Connecting");
        PhotonNetwork.ConnectUsingSettings();
    }
    void Update()
    {
        if(UIbuttons.activeSelf == true)
        {
            loading.SetActive(false);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        UIbuttons.SetActive(true);
        Debug.Log("Joined Lobby");
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameIP.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameIP.text);
    }
    public override void OnJoinedRoom()
    {
        menuManager.ToggleWindow("RoomMenu");
        loading.SetActive(false);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListPrefab>().OnStart(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        menuManager.ToggleWindow("ErrorMenu");
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        menuManager.CloseWindows();
        foreach(Transform transform in playerListContent)
        {
            Destroy(transform.gameObject);
        }

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform transform in roomListContent)
        {
            Destroy(transform.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListPrefab, roomListContent).GetComponent<RoomPrefab>().OnStart(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListPrefab>().OnStart(newPlayer);
    }

}

