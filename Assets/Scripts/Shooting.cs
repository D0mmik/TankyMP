using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPun
{
    public Load Load;
    public PauseMenu pauseMenu;
    public Item[] guns;

    void Update()
    { 
        if(!photonView.IsMine)
            return;
    
        if(Input.GetMouseButtonDown(0) && !PlayerLeave.Paused)
            guns[Load.CurrentWeapon].Use();
    }
}
