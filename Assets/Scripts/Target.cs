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
    public CapturePoint capturePoint;
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
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }
    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if(!photonView.IsMine)
        {
            return;
        }
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        capturePoint.StartScore();
        capturePoint.CheckPlayers();
        playerManager.Die();
    }
}