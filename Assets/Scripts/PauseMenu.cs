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
        randomizer = GameModes.S_Randomizer;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !PlayerLeave.Paused && !Scope.S_Scoped && !randomizer)
        {
            PauseMenuGO.SetActive(false);
            Configurator.CloseWindows();
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused && !Scope.S_Scoped && !randomizer)
        {
            PauseMenuGO.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !PlayerLeave.Paused && !Scope.S_Scoped && randomizer)
            Cursor.lockState = CursorLockMode.Locked;

        if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused && !Scope.S_Scoped && randomizer)
            Cursor.lockState = CursorLockMode.None;

    }

   


    
}
