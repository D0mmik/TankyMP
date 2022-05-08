using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Target>()?.TakeDamage(10);
            other.GetComponent<AI>()?.TakeDamage(10);
        }
        if(other.CompareTag("Wall") && photonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
