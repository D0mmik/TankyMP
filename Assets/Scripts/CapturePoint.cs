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
    Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CapturePoint" && photonView.IsMine)
        {
            capturing = true;
            hash["Counting"] = true;   
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "CapturePoint" && photonView.IsMine)
        {
            capturing = false;
            hash["Counting"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash); 
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
        if(!changedProps.ContainsKey("Counting")) return;

        CheckPlayers();
    }
    private void CheckPlayers()
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
                scoreText.text = score.ToString();
                timer = 0;

            }
        }      
    }    
    
    
}
