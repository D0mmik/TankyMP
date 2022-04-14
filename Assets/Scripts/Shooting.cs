using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPun
{
    public Transform shootPoint;
    private RaycastHit hit;
    public float damage = 10f;
    public float range = 1000f;
    private Target target;
    public Load load;

    public PauseMenu pauseMenu;
    public Item[] guns;


    void Update()
    {   if(photonView.IsMine)
        {
            if(Input.GetMouseButtonDown(0) && pauseMenu.paused == false)
            {
                guns[load.currentWeapon].Use();

            }
        }   
    }
    
}
