using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnAI : MonoBehaviourPun
{
    private GameObject ai;
    public int aiCount = 0;
    public void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        if(PhotonNetwork.IsMasterClient && aiCount < 10)
        {
            ai = PhotonNetwork.Instantiate("PlayerAI", Vector3.zero, Quaternion.identity);
            aiCount++;
        }
    }
    
    
}
    
