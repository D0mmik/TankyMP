using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnAI : MonoBehaviourPun
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && photonView.IsMine)
        {
            PhotonNetwork.Instantiate("PlayerAi", Vector3.zero, Quaternion.identity);
            Debug.Log("Spawn");

        }
    }
}
    
