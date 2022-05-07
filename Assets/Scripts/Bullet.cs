using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public float damage = 10;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && photonView.IsMine)
        {
            other.GetComponent<Target>()?.TakeDamage(damage);
            other.GetComponent<AI>()?.TakeDamage(damage);
        }
        if(!other.CompareTag("CapturePoint"))
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
