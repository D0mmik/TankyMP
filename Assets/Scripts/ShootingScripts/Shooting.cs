using Configuration;
using Photon.Pun;
using PhotonMP;
using UI;
using UnityEngine;

namespace ShootingScripts
{
    public class Shooting : MonoBehaviourPun
    {
        public Load Load;
        public PauseMenu PauseMenu;
        public Item[] Guns;

        void Update()
        { 
            if(!photonView.IsMine)
                return;
    
            if(Input.GetMouseButtonDown(0) && !PlayerLeave.Paused)
                Guns[Load.CurrentWeapon].Use();
        }
    }
}
