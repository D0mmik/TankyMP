using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class OneBarrel : Gun
{
    public Transform ShootPoint;
    private RaycastHit hit;
    private float range = 1000f;
    private Target target;
    private GameObject impact;

    private float timer;
    public float Reload;
    public bool CanShoot = true;
    public Image ReloadImage;
    public TMP_Text DamageText;
    
    private float shootCycles;
    private bool shootingProjectiles;
    private float timer2;

    public override void Use()
    {
        Shoot();
    }
    void OnEnable()
    {
        if(DamageText.text != GunInfo.Damage.ToString())
            DamageText.text = GunInfo.Damage.ToString();

        Reload = GunInfo.ReloadTime;
        RImage();
    }
    void RImage()
    {
        ReloadImage.fillAmount = Reload / GunInfo.ReloadTime;  
    }

    void Update()
    {
        timer += Time.deltaTime % 60;
        timer2 += Time.deltaTime % 60;

        if(timer >= 1)
        {
            Reload++;

            if(ReloadImage == null)
                return;

            RImage();
            timer = 0;
        }

        if(Reload < GunInfo.ReloadTime)
            CanShoot = false;
        else if(Reload == GunInfo.ReloadTime)
            CanShoot = true;

        if(shootCycles < GunInfo.Bullets && shootingProjectiles)
        {
            if(timer2 >= 0.25f)
            {
                GameObject ball =  PhotonNetwork.Instantiate("ball", ShootPoint.position, Quaternion.identity);
                ball.GetComponent<Rigidbody>().AddForce(transform.forward * 125,ForceMode.Impulse);
                shootCycles++;
                timer2 = 0;
            }
        }
        else if(shootCycles >= GunInfo.Bullets)
            shootingProjectiles = false;
    }
    public void Shoot()
    {
        if(CanShoot && !GunInfo.Projectile)
        {
            if(Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, range))
            {
                if(hit.transform != null)
                {
                    hit.transform.GetComponent<AI>()?.TakeDamage(GunInfo.Damage);
                    hit.transform.GetComponent<Target>()?.TakeDamage(GunInfo.Damage);
                }
            }
            Reload = 0;
            if(ReloadImage == null) 
                return;

            RImage();
        }

        if (!CanShoot || !GunInfo.Projectile)
            return;
        
        shootCycles = 0;
        shootingProjectiles = true;
        Reload = 0;

        if(ReloadImage == null) 
            return;

        RImage();
    }
}
