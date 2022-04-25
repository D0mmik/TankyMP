using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class AI : MonoBehaviourPun
{
    private  NavMeshAgent navMeshAgent;
    private GameObject target;
    public GameObject tower;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }     
        navMeshAgent.destination = target.transform.position;
    }
}

