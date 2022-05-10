using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class SpawnAI : MonoBehaviourPun
{
    private GameObject ai;
    public int AICount = 0;
    public int AIActive = 0;
    [SerializeField] private Transform[] spawnpoints;
    private int i = 0;

    public void Spawn()
    {
        if(PhotonNetwork.IsMasterClient && AIActive < AICount)
        {
            i = Random.Range(1,spawnpoints.Count());
            ai = PhotonNetwork.Instantiate("PlayerAI",spawnpoints[i].position, Quaternion.identity);
            AIActive++;
        }
    }
    
    
}
    
