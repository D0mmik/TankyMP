using Photon.Pun;
using PlayerScripts;
using UnityEngine;

namespace ShootingScripts
{
    public class Bullet : MonoBehaviourPun
    {
        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<Target>()?.TakeDamage(10);
                other.GetComponent<AI.AI>()?.TakeDamage(10);
            }
            if(!photonView.IsMine)
                return;

            if(other.CompareTag("Wall"))
                PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
