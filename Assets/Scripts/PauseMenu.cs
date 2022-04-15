using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPun
{
    public GameObject pauseMenu;
    public Configurator configurator;
    public ChangerIG changerIG;
    private bool randomizer = false;
    void Start()
    {
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["Randomizer"] == true)
        {
            randomizer = true;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.paused == false && Scope.scoped == false && randomizer == false)
        {
            pauseMenu.SetActive(false);
            configurator.CloseWindows();
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.paused == true && Scope.scoped == false && randomizer == false)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.paused == false && Scope.scoped == false && randomizer == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.paused == true && Scope.scoped == false && randomizer == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

   


    
}
