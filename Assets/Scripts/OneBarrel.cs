using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

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
    public TMP_Text damageText;

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
        if(damageText.text != ((GunInfo)gunInfo).damage.ToString())
        {
            damageText.text = ((GunInfo)gunInfo).damage.ToString();
            reload = 0;
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
                    hit.transform.GetComponent<AI>()?.TakeDamage(((GunInfo)gunInfo).damage);
                    hit.transform.GetComponent<Target>()?.TakeDamage(((GunInfo)gunInfo).damage);
                }
                Collider[] colliders = Physics.OverlapSphere(hit.point, 0.3f);
                if(colliders.Length != 0)
                {
                    impact = PhotonNetwork.Instantiate("impactPrefab", hit.point,Quaternion.LookRotation(hit.normal));
                    impact.transform.SetParent(colliders[0].transform);
                } 
                StartCoroutine (WaitForDestroy());
            }
            reload = 0;
            if(reloadImage == null) return;
            reloadImage.fillAmount = reload / ((GunInfo)gunInfo).reloadTime;
        }
    }
    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(5f);
        if(impact != null)
        {
            PhotonNetwork.Destroy(impact);
        }
    }
}
