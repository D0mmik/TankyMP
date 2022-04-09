using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    private bool paused = false;
    public GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            pauseMenu.SetActive(true);
            paused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            pauseMenu.SetActive(false);
            paused = false;
        }

    }
    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        if(photonView.IsMine)
        {

        }
        
        Debug.Log("eeeee");
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");
    }


    
}
