using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class PlayerManager : MonoBehaviourPun
{
    [SerializeField] private Transform[] spawnpoints;
    private int i = 0;
    GameObject controller;
    public SaveConfig saveConfig;
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        if(photonView.IsMine == true)
        {
            i = Random.Range(1,spawnpoints.Count());
            controller = PhotonNetwork.Instantiate("Player", spawnpoints[i].position, Quaternion.identity, 0, new object[]{photonView.ViewID});
        }
    }
    public void Die()
    {
        if(photonView.IsMine)
        {
            PhotonNetwork.Destroy(controller);
        }
        CreatePlayer();
    }
}
