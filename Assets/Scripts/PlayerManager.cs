using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class PlayerManager : MonoBehaviourPun
{
    [SerializeField] private Transform[] spawnpoints;
    private int i = 0;
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        if(photonView.IsMine == true)
        {
            i = Random.Range(1,spawnpoints.Count());
        Debug.Log(spawnpoints.Count());
        Debug.Log("číslo je" + i);   
        PhotonNetwork.Instantiate("PlayerRed", spawnpoints[i].position, Quaternion.identity);
        Debug.Log("Player created");
        }
    }
}
