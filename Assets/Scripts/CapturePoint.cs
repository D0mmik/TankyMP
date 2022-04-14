using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CapturePoint : MonoBehaviourPunCallbacks
{
    public bool capturing = false;
    public bool canCapture = true;
    public int score = 0;
    public TMP_Text scoreText;
    public int playerCount;
    private float timer;
    public bool allplayer = false;
    Hashtable hashtable = PhotonNetwork.LocalPlayer.CustomProperties;
    Hashtable scoreHash = new Hashtable();
    void Start()
    {
        StartScore();
        if(photonView.IsMine)
        {
            capturing = false;
            hashtable["Counting"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable); 
        }
        CheckPlayers();
    }
    public void StartScore()
    {
        if(photonView.IsMine)
        {
            score = 0;
            scoreHash = new Hashtable();
            scoreHash.Add("Score", score);
            PhotonNetwork.CurrentRoom.SetCustomProperties(scoreHash);
            scoreText.text = score.ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CapturePoint" && photonView.IsMine)
        {
            score = 0;
            capturing = true;
            hashtable["Counting"] = true;   
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "CapturePoint" && photonView.IsMine)
        {
            capturing = false;
            hashtable["Counting"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable); 
        }
    }
    void Update()
    {
        if(capturing == true && canCapture == true && photonView.IsMine)
        {
            CountScore();
        }
        if(photonView.IsMine)
        {
            if(playerCount == 1)
            {
                canCapture = true;
            }
            else if(playerCount >= 2)
            {
                canCapture = false;
            }   
        }
        
 
    }
    public override void OnPlayerPropertiesUpdate (Player targetPlayer, Hashtable changedProps)
    {
        if(!changedProps.ContainsKey("Counting"))
        {
            return;
        } 

        CheckPlayers();
    }
    public void CheckPlayers()
    {
      
        var players = PhotonNetwork.PlayerList;


        playerCount = players.Count(p => p.CustomProperties.ContainsKey("Counting") && (bool)p.CustomProperties["Counting"] == true);
        
        
    }
    void CountScore()
    {
        if(canCapture)
        {
            timer += Time.deltaTime % 60;
            if(timer >= 1)
            {
                score++;
                scoreHash = new Hashtable();
                scoreHash.Add("Score", score);
                PhotonNetwork.CurrentRoom.SetCustomProperties(scoreHash);
                timer = 0;

            }
        }      
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if(!propertiesThatChanged.ContainsKey("Score"))
        {
            return;
        } 
        scoreText.text = propertiesThatChanged["Score"].ToString();
    } 
}
