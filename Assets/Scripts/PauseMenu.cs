using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPun
{
    public GameObject PauseMenuGO;
    public Configurator Configurator;
    public ChangerIG ChangerIG;
    private bool randomizer;
    void Start()
    {
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["Randomizer"] == true)
        {
            randomizer = true;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused == false && Scope.scoped == false && randomizer == false)
        {
            PauseMenuGO.SetActive(false);
            Configurator.CloseWindows();
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused == true && Scope.scoped == false && randomizer == false)
        {
            PauseMenuGO.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused == false && Scope.scoped == false && randomizer == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused == true && Scope.scoped == false && randomizer == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

   


    
}
