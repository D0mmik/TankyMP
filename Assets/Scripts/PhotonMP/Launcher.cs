using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace PhotonMP
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] TMP_InputField RoomNameIP;
        [SerializeField] TMP_Text RoomName;
        [SerializeField] GameObject Loading;
        [SerializeField] GameObject UIButtons;
        [SerializeField] MenuManager MenuManager;
        [SerializeField] Transform RoomListContent;
        [SerializeField] GameObject RoomListPrefab;
        [SerializeField] Transform PlayerListContent;
        [SerializeField] Transform PlayerListPrefab;
        Player[] players;
        [SerializeField] GameObject StartGameButton;
        bool instagib;
        [SerializeField] GameObject CheckMarkInstagib;
        bool randomizer;
        [SerializeField] GameObject CheckMarkRandomizer;

        void Start()
        {
            Loading.SetActive(true);
            UIButtons.SetActive(false);
            CheckMarkInstagib.SetActive(false);
            CheckMarkRandomizer.SetActive(false);
            Debug.Log("Connecting");
            if(PhotonNetwork.IsConnected)
                return;   
            PhotonNetwork.ConnectUsingSettings();
        
        }
        void Update()
        {
            if(!UIButtons.activeSelf)
                return;
            Loading.SetActive(false);

        }
        public void InstagibButton()
        {
            if(!instagib)
                instagib = true;
            else if(instagib)
                instagib = false;

            CheckMarkInstagib.SetActive(instagib);
        }
        public void RandomizerButton()
        {
            if(!randomizer)
                randomizer = true;
            else if(randomizer)
                randomizer = false;

            CheckMarkRandomizer.SetActive(randomizer);
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Joined Master");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            UIButtons.SetActive(true);
            Loading.SetActive(false);
            Debug.Log("Joined Lobby");
        }
    

        public void CreateRoom()
        {
            if(string.IsNullOrEmpty(RoomNameIP.text))
                return;
        
            RoomOptions roomOptions = new RoomOptions();
            Hashtable hash = new Hashtable();
            hash["Instagib"] = instagib;
            hash["Randomizer"] = randomizer;
            roomOptions.CustomRoomProperties = hash;
            PhotonNetwork.CreateRoom(RoomNameIP.text, roomOptions);
            MenuManager.CloseWindows();
            MenuManager.PlayMenu.SetActive(false);
            Loading.SetActive(true);
        }
        public override void OnJoinedRoom()
        {
            Loading.SetActive(false);
            MenuManager.ToggleWindow("RoomMenu");
            Loading.SetActive(false);
            RoomName.text = PhotonNetwork.CurrentRoom.Name;
            players = PhotonNetwork.PlayerList;
            for(int i = 0; i < players.Count(); i++)
                Instantiate(PlayerListPrefab, PlayerListContent).GetComponent<PlayerListPrefab>().OnStart(players[i]);

            StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            MenuManager.ToggleWindow("ErrorMenu");
        }
        public void StartGame()
        {
            PhotonNetwork.LoadLevel("Game");
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            MenuManager.CloseWindows();
            Loading.SetActive(true);
            foreach(Transform transform1 in PlayerListContent)
                Destroy(transform.gameObject);
        }
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach(Transform transform1 in RoomListContent)
                Destroy(transform.gameObject);
            
            for(int i = 0; i < roomList.Count; i++)
            {
                if(roomList[i].RemovedFromList)
                    continue;
            
                Instantiate(RoomListPrefab, RoomListContent).GetComponent<RoomPrefab>().OnStart(roomList[i]);
            }
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Instantiate(PlayerListPrefab, PlayerListContent).GetComponent<PlayerListPrefab>().OnStart(newPlayer);
        }
    }
}

