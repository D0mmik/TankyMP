using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomNameIP;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject uiButtons;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject joinLobby;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListPrefab;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private Transform playerListPrefab;
    [SerializeField] private Player[] players;
    [SerializeField] private GameObject startGameButton;
    private bool instagib;
    [SerializeField] private GameObject checkMarkInstagib;
    private bool randomizer;
    [SerializeField] private GameObject checkMarkRandomizer;

    void Start()
    {
        loading.SetActive(true);
        uiButtons.SetActive(false);
        checkMarkInstagib.SetActive(false);
        checkMarkRandomizer.SetActive(false);
        Debug.Log("Connecting");
        if(PhotonNetwork.IsConnected)
            return;   
        PhotonNetwork.ConnectUsingSettings();
        
    }
    void Update()
    {
        if(!uiButtons.activeSelf)
            return;
        loading.SetActive(false);

    }
    public void InstagibButton()
    {
        if(!instagib)
            instagib = true;
        else if(instagib)
            instagib = false;

        checkMarkInstagib.SetActive(instagib);
    }
    public void RandomizerButton()
    {
        if(!randomizer)
            randomizer = true;
        else if(randomizer)
            randomizer = false;

        checkMarkRandomizer.SetActive(randomizer);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        uiButtons.SetActive(true);
        loading.SetActive(false);
        Debug.Log("Joined Lobby");
    }
    

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameIP.text))
            return;
        
        RoomOptions roomOptions = new RoomOptions();
        Hashtable hash = new Hashtable();
        hash["Instagib"] = instagib;
        hash["Randomizer"] = randomizer;
        roomOptions.CustomRoomProperties = hash;
        PhotonNetwork.CreateRoom(roomNameIP.text, roomOptions);
        menuManager.CloseWindows();
        menuManager.PlayMenu.SetActive(false);
        loading.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        loading.SetActive(false);
        menuManager.ToggleWindow("RoomMenu");
        loading.SetActive(false);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Count(); i++)
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListPrefab>().OnStart(players[i]);

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
        loading.SetActive(true);
        foreach(Transform transform in playerListContent)
            Destroy(transform.gameObject);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform transform in roomListContent)
            Destroy(transform.gameObject);
            
        for(int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList)
               continue;
            
            Instantiate(roomListPrefab, roomListContent).GetComponent<RoomPrefab>().OnStart(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListPrefab>().OnStart(newPlayer);
    }
}

