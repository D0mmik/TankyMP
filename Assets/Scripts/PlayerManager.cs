using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPun
{
    private void Start()
    {
        if(photonView.IsMine)
        {
            CreatePlayer();
        }
    }
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        Debug.Log("Player created");
    }
}
