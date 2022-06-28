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
        [Header("Texts")]
        [SerializeField] TMP_Text ScoreText;
        [SerializeField] TMP_Text WinnerText;
        [SerializeField] GameObject WinnerGameObject;
        Hashtable hashtable = PhotonNetwork.LocalPlayer.CustomProperties;  
        Hashtable scoreHash = new Hashtable();
        float score;
        int playerCount;
        float timer;
        bool capturing;
        bool canCapture = true;
        void Start()
        {
            StartScore();
            if(!photonView.IsMine)
                return;
        
            capturing = false;
            hashtable["Counting"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);

            CheckPlayers();
        }
        public void StartScore()
        {
            if(!photonView.IsMine)
                return;
        
            score = 0;
            scoreHash = new Hashtable();
            scoreHash.Add("Score", score);
            PhotonNetwork.CurrentRoom.SetCustomProperties(scoreHash);
            ScoreText.text = score.ToString();
        
        }

        void OnTriggerEnter(Collider other)
        {
            if(!photonView.IsMine)
                return;

            if(other.gameObject.CompareTag("CapturePoint"))
            {
                score = 0;
                capturing = true;
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
        
            capturing = false;
            hashtable["Counting"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
        void Update()
        {
            if(!photonView.IsMine)
                return;

            if(capturing && canCapture)
                CountScore();
        
            if(playerCount == 1)
                canCapture = true;
            else if(playerCount >= 2)
                canCapture = false;
        
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
            playerCount = players.Count(p => p.CustomProperties.ContainsKey("Counting") && (bool)p.CustomProperties["Counting"]);
        }

        void CountScore()
        {
            if(!canCapture)
                return; 
        
            timer += Time.deltaTime % 60;
            if(timer >= 1)
            {
                score++;
                scoreHash = new Hashtable();
                if(score >= 20)
                    score = 20;
                
                scoreHash.Add("Score", score);
                PhotonNetwork.CurrentRoom.SetCustomProperties(scoreHash);
                timer = 0;   
            }      
        }
        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if(!propertiesThatChanged.ContainsKey("Score"))
                return;
         
            ScoreText.text = propertiesThatChanged["Score"].ToString();

            if ((float)propertiesThatChanged["Score"] != 20 || playerCount != 1) 
                return;
            
            var players = PhotonNetwork.PlayerList;
            score = 0;

            if(!photonView.IsMine)
                return;
        
            Player winner = players.Single(p => p.CustomProperties.ContainsKey("Counting") && (bool)p.CustomProperties["Counting"]);
            WinnerGameObject.SetActive(true);
            WinnerText.text = ($"{winner.NickName} CAPTURED POINT");
            StartCoroutine(HideWinnerGo());
        } 
        IEnumerator HideWinnerGo()
        {
            yield return new WaitForSeconds(5);
            WinnerGameObject.SetActive(false);
        }
    }
}
