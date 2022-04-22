using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class OneBarrel : Gun
{
    public Transform shootPoint;
    private RaycastHit hit;
    private float range = 1000f;
    private Target target;
    private GameObject impact;

    private float timer;
    public float reload;
    public bool canShoot = true;
    public Image reloadImage;

    public override void Use()
    {
        Shoot();
    }
    void Update()
    {
        timer += Time.deltaTime % 60;
        if(timer >= 1)
        {
            reload++;
            if(reloadImage == null) return;
            reloadImage.fillAmount = reload / ((GunInfo)gunInfo).reloadTime;
            timer = 0;
        }
        if(reload < ((GunInfo)gunInfo).reloadTime)
        {
            canShoot = false;
        }
        else if(reload == ((GunInfo)gunInfo).reloadTime)
        {
            canShoot = true;
        }
    }
    public void Shoot()
    {
        if(canShoot == true)
        {
            if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
            {
                if(hit.transform != null)
                {
                    hit.transform.GetComponent<Target>()?.TakeDamage(((GunInfo)gunInfo).damage);
                }
                impact = PhotonNetwork.Instantiate("impactPrefab", hit.point,Quaternion.LookRotation(hit.normal));
                Destroy(impact,5);
            }
            reload = 0;
            if(reloadImage == null) return;
            reloadImage.fillAmount = reload / ((GunInfo)gunInfo).reloadTime;
        }
    }
}
