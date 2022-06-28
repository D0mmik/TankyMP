using Photon.Pun;
using PhotonMP;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class Target : MonoBehaviourPun
    {
        public float Health = 100;
        public float MaxHealth = 100;
        public Image HealthBar;
        PlayerManager playerManager;
        public CapturePoint.CapturePoint CapturePoint;
        public GameObject Armor;
        public bool Armored;
        bool instagib;
        void Start()
        {
            playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
            instagib = GameModes.SInstagib;
            if(!photonView.IsMine)
                return;
        
            switch (instagib)
            {
                case false:
                    Health = MaxHealth;
                    HealthBar.fillAmount = Health / MaxHealth;
                    break;
                case true:
                    Health = 1;
                    MaxHealth = 1;
                    break;
            }
        }
        void Update()
        {
            if(!photonView.IsMine && instagib)
                return;

            if(Armor.activeSelf && !Armored)
            {
                Armored = true;
                Health = 200;
                MaxHealth = 200;
                HealthBar.fillAmount = Health / MaxHealth;
            }
            else if(!Armor.activeSelf && Armored)
            {   
                Armored = false;
                MaxHealth = 100;
                if(Health >= 100)
                {
                    Health = 100;
                }
                HealthBar.fillAmount = Health / MaxHealth;
        
            }
        }
    
        public void TakeDamage(float damage)
        {
            if(Health >= 0)
                photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        }
        [PunRPC]
        void RPC_TakeDamage(float damage)
        {
            if(!photonView.IsMine)
                return;

            Health -= damage;
            HealthBar.fillAmount = Health / MaxHealth;
        
            if(Health <= 0)
            {
                Die();
            }
        }
        void Die()
        {
            CapturePoint.StartScore();
            CapturePoint.CheckPlayers();
            playerManager.Die();
        }
    }
}