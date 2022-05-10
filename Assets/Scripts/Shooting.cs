using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPun
{
    public Transform ShootPoint;
    private RaycastHit hit;
    public float Damage = 10f;
    public float Range = 1000f;
    private Target target;
    public Load Load;
    public OneBarrel OneBarrel;

    public PauseMenu pauseMenu;
    public Item[] guns;

    void Update()
    { 
        if(photonView.IsMine)
        {
            if(Input.GetMouseButtonDown(0) && PlayerLeave.Paused == false)
            {
                guns[Load.CurrentWeapon].Use();
            }
        }   
    }
}
