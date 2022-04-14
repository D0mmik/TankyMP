using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPun
{
    public bool paused = false;
    public GameObject pauseMenu;
    public Configurator configurator;
    public Scope scope;
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && paused == false && photonView.IsMine && scope.scoped == false)
        {
            pauseMenu.SetActive(true);
            configurator.CloseWindows();
            paused = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true && photonView.IsMine && scope.scoped == false)
        {
            pauseMenu.SetActive(false);
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

   


    
}
