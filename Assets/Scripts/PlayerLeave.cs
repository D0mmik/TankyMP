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
    private bool paused = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            LeaveButton.SetActive(true);
            paused = true;

        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            LeaveButton.SetActive(false);
            paused = false;
        }
    }
    public void Leave()
    {
        Destroy(RoomManager.Instance.gameObject);
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");  
    }
}
