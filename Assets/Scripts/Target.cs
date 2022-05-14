using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class Target : MonoBehaviourPun
{
    public float Health = 100;
    public float MaxHealth = 100;
    public Image HealthBar;
    PlayerManager PlayerManager;
    public CapturePoint CapturePoint;
    public GameObject Armor;
    public bool Armored;
    private bool instagib;
    void Start()
    {
        PlayerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        instagib = GameModes.S_Instagib;
        if(!photonView.IsMine)
            return;
        
        if(!instagib)
        {
            Health = MaxHealth;
            HealthBar.fillAmount = Health / MaxHealth;
        }
        if(instagib)
        {
            Health = 1;
            MaxHealth = 1;
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
        PlayerManager.Die();
    }
}