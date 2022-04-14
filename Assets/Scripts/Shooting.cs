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

    public PauseMenu pauseMenu;


    void Update()
    {   if(photonView.IsMine)
        {
            if(Input.GetMouseButtonDown(0) && pauseMenu.paused == false)
            {
                photonView.RPC("RPC_Shoot",RpcTarget.All);
            }
        }   
    }
    [PunRPC]
    void RPC_Shoot()
    {
        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
        {
            if(hit.transform != null)
            {
                hit.transform.GetComponent<Target>()?.TakeDamage(damage);
            }
            if(photonView.IsMine)
            {
                PhotonNetwork.Instantiate("impactPrefab", hit.point,Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
