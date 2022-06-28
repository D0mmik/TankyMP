using Configuration;
using Photon.Pun;
using PhotonMP;
using PlayerScripts;
using ShootingScripts;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviourPun
    {
        public GameObject PauseMenuGO;
        public Configurator Configurator;
        private bool _randomizer;
        void Start()
        {
            _randomizer = GameModes.SRandomizer;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !PlayerLeave.Paused && !Scope.SScoped && !_randomizer)
            {
                PauseMenuGO.SetActive(false);
                Configurator.CloseWindows();
                Cursor.lockState = CursorLockMode.Locked;
            }
            if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused && !Scope.SScoped && !_randomizer)
            {
                PauseMenuGO.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            if(Input.GetKeyDown(KeyCode.Escape) && !PlayerLeave.Paused && !Scope.SScoped && _randomizer)
                Cursor.lockState = CursorLockMode.Locked;

            if(Input.GetKeyDown(KeyCode.Escape) && PlayerLeave.Paused && !Scope.SScoped && _randomizer)
                Cursor.lockState = CursorLockMode.None;

        }

   


    
    }
}
