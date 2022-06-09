using System.Collections;
using System.Globalization;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace CapturePoint
{
    public class CapturePoint : MonoBehaviourPunCallbacks
    {
        private bool _capturing = false;
        private bool _canCapture = true;
        private float _score;
        public TMP_Text ScoreText;
        private int _playerCount;
        private float _timer;
        public TMP_Text WinnerText;
        public GameObject WinnerGameObject;
        Hashtable hashtable = PhotonNetwork.LocalPlayer.CustomProperties;  
        Hashtable scoreHash = new Hashtable();
        void Start()
        {
            StartScore();
            if(!photonView.IsMine)
                return;
        
            _capturing = false;
            hashtable["Counting"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);

            CheckPlayers();
        }
        public void StartScore()
        {
            if(!photonView.IsMine)
                return;
        
            _score = 0;
            scoreHash = new Hashtable();
            scoreHash.Add("Score", _score);
            PhotonNetwork.CurrentRoom.SetCustomProperties(scoreHash);
            ScoreText.text = _score.ToString();
        
        }

        void OnTriggerEnter(Collider other)
        {
            if(!photonView.IsMine)
                return;

            if(other.gameObject.CompareTag("CapturePoint"))
            {
                _score = 0;
                _capturing = true;
                hashtable["Counting"] = true;   
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
            }
        }
        void OnTriggerExit(Collider other)
        {
            if(!photonView.IsMine)
                return;

            if (!other.gameObject.CompareTag("CapturePoint")) 
                return;
        
            _capturing = false;
            hashtable["Counting"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
        void Update()
        {
            if(!photonView.IsMine)
                return;

            if(_capturing && _canCapture)
                CountScore();
        
            if(_playerCount == 1)
                _canCapture = true;
            else if(_playerCount >= 2)
                _canCapture = false;
        
        }
        public override void OnPlayerPropertiesUpdate (Player targetPlayer, Hashtable changedProps)
        {
            if(!changedProps.ContainsKey("Counting"))
                return;

            CheckPlayers();
        }

        public void CheckPlayers()
        {
            var players = PhotonNetwork.PlayerList;
            _playerCount = players.Count(p => p.CustomProperties.ContainsKey("Counting") && (bool)p.CustomProperties["Counting"]);
        }

        void CountScore()
        {
            if(!_canCapture)
                return; 
        
            _timer += Time.deltaTime % 60;
            if(_timer >= 1)
            {
                _score++;
                scoreHash = new Hashtable();
                if(_score >= 20)
                    _score = 20;
                
                scoreHash.Add("Score", _score);
                PhotonNetwork.CurrentRoom.SetCustomProperties(scoreHash);
                _timer = 0;   
            }      
        }
        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if(!propertiesThatChanged.ContainsKey("Score"))
                return;
         
            ScoreText.text = propertiesThatChanged["Score"].ToString();

            if((float)propertiesThatChanged["Score"] == 20 && _playerCount == 1)
            {
                var players = PhotonNetwork.PlayerList;
                _score = 0;

                if(!photonView.IsMine)
                    return;
        
                Player winner = players.Single(p => p.CustomProperties.ContainsKey("Counting") && (bool)p.CustomProperties["Counting"]);
                WinnerGameObject.SetActive(true);
                WinnerText.text = ($"{winner.NickName} CAPTURED POINT");
                StartCoroutine(HideWinnerGO());
        

            }
        } 
        IEnumerator HideWinnerGO()
        {
            yield return new WaitForSeconds(5);
            WinnerGameObject.SetActive(false);
        }
    }
}
