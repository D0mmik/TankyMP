using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LeaveGame : MonoBehaviourPunCallbacks
{
    public void Leave()
    {
        if(!photonView.IsMine)
            return;

        Destroy(RoomManager.S_RoomManager.gameObject);
        PhotonNetwork.LeaveRoom();
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(!photonView.IsMine)
            return;     
        SceneManager.LoadScene("Menu");
    }
}
