using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerLeave : MonoBehaviourPunCallbacks
{
    public GameObject LeaveButton;
    public GameObject Bar;
    public static bool Paused = false;
    void Start()
    {
        LeaveButton.SetActive(false);
        Paused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !Paused && !Scope.S_Scoped)
        {
            LeaveButton.SetActive(true);
            Paused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Paused)
        {
            LeaveButton.SetActive(false);
            Paused = false;
        }
    }
    public void Leave()
    {
        Destroy(RoomManager.S_RoomManager.gameObject);
        Destroy(Bar);
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");  
    }
}
