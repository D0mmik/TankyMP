using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class Target : MonoBehaviourPun
{
    public float health = 100;
    public float maxHealth = 100;
    public Image healthBar;
    PlayerManager playerManager;
    public CapturePoint capturePoint;
    public GameObject armor;
    public bool armored = false;
    private bool instagib = false;
    void Awake()
    {
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["Instagib"] == true)
        {
            instagib = true;
        }
        if(photonView.IsMine)
        {
            if(instagib == false)
            {
                health = maxHealth;
                healthBar.fillAmount = health / maxHealth;
            }
            if(instagib == true)
            {
                health = 1;
                maxHealth = 1;
            }
        }
    }
    void Update()
    {
        if(photonView.IsMine && instagib == false)
        {
            if(armor.activeSelf == true && armored == false)
            {
                armored = true;
                health = 200;
                maxHealth = 200;
                healthBar.fillAmount = health / maxHealth;
            }
            else if(armor.activeSelf == false && armored == true)
            {   
                armored = false;
                maxHealth = 100;
                if(health >= 100)
                {
                    health = 100;
                }
                healthBar.fillAmount = health / maxHealth;
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        if(health >= 0)
        {
            photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        }
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