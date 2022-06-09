using Photon.Pun;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootingScripts
{
    public class OneBarrel : Gun
    {
        public Transform ShootPoint;
        private RaycastHit hit;
        private float _range = 1000f;
        private Target _target;
        private GameObject _impact;

        private float _timer;
        public float Reload;
        public bool CanShoot = true;
        public Image ReloadImage;
        public TMP_Text DamageText;
    
        private float _shootCycles;
        private bool _shootingProjectiles;
        private float _timer2;

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
            _timer += Time.deltaTime % 60;
            _timer2 += Time.deltaTime % 60;

            if(_timer >= 1)
            {
                Reload++;

                if(ReloadImage == null)
                    return;

                RImage();
                _timer = 0;
            }

            if(Reload < GunInfo.ReloadTime)
                CanShoot = false;
            else if(Reload == GunInfo.ReloadTime)
                CanShoot = true;

            if(_shootCycles < GunInfo.Bullets && _shootingProjectiles)
            {
                if(_timer2 >= 0.25f)
                {
                    GameObject ball =  PhotonNetwork.Instantiate("ball", ShootPoint.position, Quaternion.identity);
                    ball.GetComponent<Rigidbody>().AddForce(transform.forward * 125,ForceMode.Impulse);
                    _shootCycles++;
                    _timer2 = 0;
                }
            }
            else if(_shootCycles >= GunInfo.Bullets)
                _shootingProjectiles = false;
        }
        public void Shoot()
        {
            if(CanShoot && !GunInfo.Projectile)
            {
                if(Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, _range))
                {
                    if(hit.transform != null)
                    {
                        hit.transform.GetComponent<AI.AI>()?.TakeDamage(GunInfo.Damage);
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
        
            _shootCycles = 0;
            _shootingProjectiles = true;
            Reload = 0;

            if(ReloadImage == null) 
                return;

            RImage();
        }
    }
}
