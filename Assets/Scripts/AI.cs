using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class AI : MonoBehaviourPun
{
    private  NavMeshAgent navMeshAgent;
    private GameObject opponentTarget;

    public float health = 100;


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        opponentTarget = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(opponentTarget == null)
        {
            opponentTarget = GameObject.FindGameObjectWithTag("Player");
        }     
        navMeshAgent.destination = opponentTarget.transform.position;
       
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
        
        if(health <= 0)
        {
            photonView.RPC("RPC_Destroy", RpcTarget.MasterClient); 
        }
    }

    [PunRPC]
    void RPC_Destroy()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}

