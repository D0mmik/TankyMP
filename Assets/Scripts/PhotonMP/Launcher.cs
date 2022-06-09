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
        [SerializeField] private TMP_Text roomNameIP;
        [SerializeField] private TMP_Text roomName;
        [SerializeField] private GameObject loading;
        [SerializeField] private GameObject uiButtons;
        [SerializeField] private MenuManager menuManager;
        [SerializeField] private Transform roomListContent;
        [SerializeField] private GameObject roomListPrefab;
        [SerializeField] private Transform playerListContent;
        [SerializeField] private Transform playerListPrefab;
        [SerializeField] private Player[] _players;
        [SerializeField] private GameObject startGameButton;
        private bool _instagib;
        [SerializeField] private GameObject checkMarkInstagib;
        private bool _randomizer;
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
            if(!_instagib)
                _instagib = true;
            else if(_instagib)
                _instagib = false;

            checkMarkInstagib.SetActive(_instagib);
        }
        public void RandomizerButton()
        {
            if(!_randomizer)
                _randomizer = true;
            else if(_randomizer)
                _randomizer = false;

            checkMarkRandomizer.SetActive(_randomizer);
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
            hash["Instagib"] = _instagib;
            hash["Randomizer"] = _randomizer;
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
            _players = PhotonNetwork.PlayerList;
            for(int i = 0; i < _players.Count(); i++)
                Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListPrefab>().OnStart(_players[i]);

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
            foreach(Transform transform1 in playerListContent)
                Destroy(transform.gameObject);
        }
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach(Transform transform1 in roomListContent)
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
}

