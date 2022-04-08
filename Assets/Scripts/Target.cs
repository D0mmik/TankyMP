using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviourPun
{
    public float health = 100;
    PlayerManager playerManager;
    void Awake()
    {
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if(health == 0)
        {
            Die();
        }

    }
    void Die()
    {
        playerManager.Die();
    }
}