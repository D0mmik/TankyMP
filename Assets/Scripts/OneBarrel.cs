using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OneBarrel : Gun
{
    private PhotonView photonView;
    public Transform shootPoint;
    private RaycastHit hit;
    private float range = 1000f;
    private Target target;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public override void Use()
    {
        Shoot();
    }
    public void Shoot()
    {
        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
        {
            if(hit.transform != null)
            {
                hit.transform.GetComponent<Target>()?.TakeDamage(((GunInfo)gunInfo).damage);
            }
            PhotonNetwork.Instantiate("impactPrefab", hit.point,Quaternion.LookRotation(hit.normal));
        }
    }
}
