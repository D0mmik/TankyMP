using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Target : MonoBehaviourPun
{
    public float health = 100;
    public float maxHealth = 100;
    public Image healthBar;
    PlayerManager playerManager;
    void Awake()
    {
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        if(photonView.IsMine)
        {
            health = maxHealth;
        }
    }
    public void TakeDamage(float damage)
    {
        if(photonView.IsMine)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
        
            if(health == 0)
            {
                Die();
            }
        }

    }
    void Die()
    {
        playerManager.Die();
    }
}