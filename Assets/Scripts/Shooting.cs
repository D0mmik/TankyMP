using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPun
{
    public Transform shootPoint;
    private RaycastHit hit;
    public float damage = 10f;
    public float range = 100f;
    public GameObject impactPrefab;
    private Target target;
    void Update()
    {   if(photonView.IsMine)
        {
            if(Input.GetMouseButtonDown(0))
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
            target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
               target.TakeDamage(damage);
            }

            Instantiate(impactPrefab, hit.point,Quaternion.LookRotation(hit.normal));
        }
    }
}
